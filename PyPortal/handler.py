#!/usr/bin/python
# -*- coding: utf-8 -*-
"""
 @author:   hty0807@gmail.com
"""
import sys
import os
import json
import tornado.web
import datetime
import hashlib
import base64
import constants
import util
from ooc import soap, factory
from jsonutil import default
from random import random

freemethods = ['logging']
sessions = {}
SESSION_NAME = 'portal_sessid'

def get_session(self):
    sessionid = self.get_cookie(SESSION_NAME)
    if sessionid not in sessions:
        sessionid = md5("sessionkey:%s" % random())
        self.set_cookie(SESSION_NAME, sessionid)
        sessions[sessionid] = {}
        #sessions[sessionid] = {'userid': '1', 'username': 'test'}
    return sessions[sessionid]

def md5(key):
    return hashlib.md5(key).hexdigest()

def check_privilege(self, method):
    if method in freemethods:
        return True
    return is_logged(self)

def is_logged(self):
    return (get_userid(self) is not None)

def get_userid(self):
    userid, username = get_user(self)
    return userid

def get_username(self):
    userid, username = get_user(self)
    return username

def get_user(self):
    session = get_session(self)
    if 'userid' not in session:
        return None, None
    return session['userid'], session['username']

class ApiHandler(tornado.web.RequestHandler):

    def callback(self, obj):
        self.write(json.dumps(obj, default=default))

    def api_logging(self):
        username = self.get_argument('username')
        password = self.get_argument('password')
        success = soap['UserService'].Auth(username, password)
        if success:
            user = soap['UserService'].GetByUsername(username)
            session = get_session(self)
            session['userid'] = user.id
            session['username'] = user.username
            self.callback({'success': 1})
        else:
            self.callback({'success': 0})

    def api_session(self):
        session = get_session(self)
        self.callback(session)

    def api_update_password(self):
        password = self.get_argument('password')
        soap['UserService'].UpdatePassword(self.userid, password)
        self.callback({'success': 1})

    def api_file_head(self):
        fileName = self.get_argument('fileName')
        headContent = soap['FileService'].Head(fileName, 10)
        self.callback({'success': 1, 'fileName': fileName, 'headContent': headContent})

    def api_task_status(self):
        guid = self.get_argument('guid')
        task = soap['TaskService'].QueryTaskByGuid(guid)
        progress = util.kvs2dict(soap['TaskService'].QueryModelProgressByGuid(guid))
        modelName = {}
        for k in progress:
            modelName[k] = soap['CompositionService'].GetCompositionModelData(k).Model.name
        self.callback({
            'success': 1, 
            'state': task.state, 
            'stateText': constants.get_task_state(task.state),
            'progress': progress,
            'modelName': modelName
        })

    def api_composition_data(self):
        guid = self.get_argument('guid')
        compositionData = soap['CompositionService'].GetCompositionData(guid)
        self.callback({'success': 1, 'data': util.recursive_asdict(compositionData)})

    def api_composition_set_model_position(self):
        guid = self.get_argument('guid')
        x = self.get_argument('x')
        y = self.get_argument('y')
        soap['CompositionService'].UpdateCompositionModelProperty(guid, 'x', x)
        soap['CompositionService'].UpdateCompositionModelProperty(guid, 'y', y)
        self.callback({'success': 1})

    def api_composition_set_title(self):
        guid = self.get_argument('guid')
        title = self.get_argument('title')
        soap['CompositionService'].UpdateCompositionTitle(guid, title)
        self.callback({'success': 1})

    def api_composition_remove_model(self):
        guid = self.get_argument('guid')
        soap['CompositionService'].RemoveModel(guid)
        self.callback({'success': 1})

    def api_composition_add_link(self):
        compositionGuid = self.get_argument('compositionGuid')
        sourceCmGuid = self.get_argument('sourceCmGuid')
        targetCmGuid = self.get_argument('targetCmGuid')
        sourceQuantity = self.get_argument('sourceQuantity')
        targetQuantity = self.get_argument('targetQuantity')
        sourceElementSet = self.get_argument('sourceElementSet')
        targetElementSet = self.get_argument('targetElementSet')
        soap['CompositionService'].CreateCompositionLink(compositionGuid, sourceCmGuid, targetCmGuid, sourceQuantity, targetQuantity, sourceElementSet, targetElementSet, {})
        self.callback({'success': 1})

    def api_create_task(self):
        compositionGuid = self.get_argument('compositionGuid')
        userId = get_userid(self)
        triggerInvokeTime = self.get_argument('triggerInvokeTime')
        taskGuid = soap['TaskService'].Create(compositionGuid, userId, triggerInvokeTime)
        self.callback({'success': 1, 'taskGuid': taskGuid})

    def api_run_task(self):
        guid = self.get_argument('guid')
        soap['TaskService'].UpdateState(guid, "Ready")
        self.callback({'success': 1})

    def process(self, method):
        if not check_privilege(self, method):
            self.callback({'success': 0, 'error': 'ACCESS_DENIED'})
            return
        self.userid, self.username = get_user(self)
        try:
            delegate = getattr(self, 'api_%s' % method)
        except:
            raise tornado.web.HTTPError(404)
        delegate()

    def post(self, method):
        self.process(method)
        
    def get(self, method):
        self.process(method)

class PortalHandler(tornado.web.RequestHandler):

    def _render(self, tpl, **kwargs):
        kwargs['current_user'] = get_username(self)
        self.render(tpl, **kwargs)

    def portal_home(self):
        if is_logged(self):
            self.portal_dashboard()
        else:
            self.portal_logging()

    def portal_account(self):
        user = soap['UserService'].GetByUsername(get_username(self))
        self._render('account.html', user=user)

    def portal_logging(self):
        self._render('logging.html')

    def portal_dashboard(self):
        self._render('dashboard.html')

    def portal_task_my(self):
        tasks = soap['TaskService'].QueryTaskByUserId(get_userid(self))[0]
        self._render('task_my.html', tasks=tasks)

    def portal_task_status(self, guid):
        task = soap['TaskService'].QueryTaskByGuid(guid)
        self._render('task_status.html', task=task)

    def portal_task_files(self, guid):
        output_files = soap['TaskService'].QueryTaskFileMapping(guid, "Output")[0]
        log_files = soap['TaskService'].QueryTaskFileMapping(guid, "Log")[0]
        stats = {}
        for f in output_files:
            stats[f.fileName] = soap['FileService'].Stat(f.fileName)
        for f in log_files:
            stats[f.fileName] = soap['FileService'].Stat(f.fileName)
        self._render('task_files.html', output_files=output_files, log_files=log_files, stats=stats)

    def portal_composition_my(self):
        compositions = soap['CompositionService'].QueryCompositionByAuthorUserId(get_userid(self))[0]
        self._render('composition_my.html', compositions=compositions)

    def portal_composition_view(self, guid):
        self._render('composition_view.html', guid=guid)

    def portal_composition_new(self):
        guid = soap['CompositionService'].Create(get_userid(self), "Untitled Composition", False, False)
        self.redirect('/composition/view/' + guid)

    def portal_composition_addModel(self, modelGuid, compositionGuid):
        properties = soap['ModelService'].GetModelProperties(modelGuid)[0]
        model = soap['ModelService'].GetByGuid(modelGuid)
        self._render('composition_add_model.html', compositionGuid=compositionGuid, modelGuid=modelGuid, model=model, properties=properties)

    def portal_composition_doAddModel(self):
        compositionGuid = self.get_argument('compositionGuid')
        modelGuid = self.get_argument('modelGuid')
        properties = soap['ModelService'].GetModelProperties(modelGuid)[0]
        kvs = {}
        for p in properties:
            if p.type == 4:
                f = self.request.files[p.key][0]
                body = base64.b64encode(f['body'])
                fileName = soap['CompositionService'].GenerateInputFileName(compositionGuid, modelGuid, f['filename'])
                soap['FileService'].Put(fileName, body)
                kvs[p.key] = fileName
            else:
                kvs[p.key] = self.get_argument(p.key)
        cmGuid = soap['CompositionService'].CreateCompositionModel(compositionGuid, modelGuid, None)
        for key in kvs:
            soap['CompositionService'].UpdateCompositionModelProperty(cmGuid, key, kvs[key])
        self.redirect('/composition/view/' + compositionGuid)

    def portal_task_file_download(self, *args):
        fileName = self.get_argument('fileName')
        stat = soap['FileService'].Stat(fileName)
        size = stat.Size
        offset = 0
        self.set_header('Content-Type', 'application/x-octet-stream')
        self.set_header('Content-Length', size)
        while offset < size:
            ''' chunk size: 1MB '''
            chunkSize = min(1024 * 1024, size - offset)
            chunk = soap['FileService'].Read(fileName, offset, chunkSize)
            offset += chunk.Length
            self.write(base64.b64decode(chunk.Chunk))
        self.finish()

    def portal_logout(self):
        session = get_session(self)
        if 'userid' in session:
            del session['userid']
        if 'username' in session:
            del session['username']
        self.redirect('/')

    def process(self, path):
        if not path:
            path = 'home'
        dirs = path.split('/')
        delegate = None
        args = []
        while len(dirs) > 0:
            lookup = '_'.join(dirs)
            try:
                delegate = getattr(self, 'portal_%s' % lookup)
                break
            except:
                args.append(dirs[len(dirs)-1])
                dirs = dirs[0:len(dirs)-1]
                continue
        if not delegate:
            raise tornado.web.HTTPError(404)
        delegate(*args)

    def post(self, path):
        self.process(path)

    def get(self, path):
        self.process(path)
