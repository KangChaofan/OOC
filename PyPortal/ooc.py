#!/usr/bin/python
# -*- coding: utf-8 -*-
"""
 @author:   hty0807@gmail.com
"""
import service

soap = {}
factory = {}
OOC_HOST = '10.211.55.5'
OOC_SERVICE_NAMES = ['BillService', 'CompositionService', 'FileService', 'TaskService', 'UserService']

for name in OOC_SERVICE_NAMES:
    soap[name], factory[name] = service.getService(OOC_HOST, name)
