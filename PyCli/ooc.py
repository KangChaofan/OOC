#!/usr/bin/python
# -*- coding: utf-8 -*-
"""
 @author:   hty0807@gmail.com
"""
import service
ooc = {}

OOC_SERVICE_NAMES = ['BillService', 'CompositionService', 'FileService', 'TaskService', 'UserService']

def init(host):
    for name in OOC_SERVICE_NAMES:
        ooc[name] = service.getService(host, name)
