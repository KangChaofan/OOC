#!/usr/bin/python
# -*- coding: utf-8 -*-
"""
 @author:   hty0807@gmail.com
"""
task_state = {
    0: 'Created', 
    1: 'Ready', 
    2: 'Assigned', 
    3: 'Running', 
    4: 'Finishing', 
    5: 'Aborted', 
    6: 'Completed'
}

def get_task_state(s):
    return task_state[s]
