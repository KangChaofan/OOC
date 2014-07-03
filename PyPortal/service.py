#!/usr/bin/python
# -*- coding: utf-8 -*-
"""
 @author:   hty0807@gmail.com
"""
from suds.client import Client
from suds.sax.element import Element

def getService(host, name):
    print 'Reading wsdl from %s' % name
    url = 'http://%s:6736/Service/%s.svc?wsdl' % (host, name)
    client = Client(url)
    return client.service, client.factory
