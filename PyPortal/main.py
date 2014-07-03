#!/usr/bin/python
# -*- coding: utf-8 -*-
"""
 @author:   hty0807@gmail.com
"""
import sys
import os
import json
import tornado.web
import tornado.template
from handler import ApiHandler, PortalHandler
from urlparse import urlparse

static_path = 'static'

listen_port = 4444

application = tornado.web.Application([
        (r"/api/(.*)", ApiHandler),
        (r"/static/(.*)", tornado.web.StaticFileHandler, {'path': static_path}),
        (r"/(.*)", PortalHandler),
])

application.settings['template_path'] = 'tpl'

if __name__ == "__main__":

    if len(sys.argv) == 2:
        LISTEN_PORT = int(sys.argv[1])
    
    application.listen(listen_port, "0.0.0.0")
    tornado.ioloop.IOLoop.instance().start()
