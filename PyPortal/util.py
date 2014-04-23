#!/usr/bin/python
# -*- coding: utf-8 -*-
"""
 @author:   hty0807@gmail.com
"""
def kvs2dict(Kvs):
    dic = {}
    for kv in Kvs.Kvs[0]:
        dic[kv.Key] = kv.Value
    return dic
