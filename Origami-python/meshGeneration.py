# This tool is designed for easy mesh(.obj) generation

import os
import numpy as np

root_directory = os.path.curdir

# List of all parameters:

separate_file = True

file_name = "crane"
x = (np.tan(3.0/16.0*np.pi)-np.tan(np.pi/4.0))/(1+np.tan(5/16.0*np.pi))
y = 0.5*(1-np.tan(np.pi/4.0))
z = 1-np.tan(3.0/8.0*np.pi)
vlist = [(-1, 1),  # A
         (-1, 0),  # B
         (np.tan(np.pi/16.0)-1, 0),  # C
         (np.tan(np.pi/8.0)-1, 0),  # D
         (np.tan(np.pi/8.0)-1+x, x),  # E
         (-y, y),  # F
         (0, 0),  # G
         (-z/4.0, z/4.0),  # H
         (-z/2.0, 0),  # I
         (0, z/2.0),  # K
         (x, 1-x-np.tan(np.pi / 8.0)),  # J
         (),
         (),
         (), ]
tlist = [(1, 2, 3), (2, 3, 4)]  # order for the vertices should be anti-clockwise for

scale = 1


def generate(file_name, vlist, tlist):
    with open(file_name, "w") as fptr:
        fptr.write("# File: '" + file_name + "'")
        fptr.write("o " + (file_name.split("/")[-1]).split(".")[0])
        for v in vlist:
            fptr.write("v {} {} 0.0".format(v[0] * scale, v[1] * scale))
        for t in tlist:
            fptr.write("f {} {} {}".format(t[0], t[1], t[2]))


def run():
    if not separate_file:
        for i in range(len(tlist)):
            triangle = tlist[i]
            vlist_separate = [vlist[triangle[0]], vlist[triangle[1]], vlist[triangle[2]]]
            generate(os.path.join(root_directory, file_name+f'{i:03}'+".obj"), vlist_separate, triangle)
    else:
        generate(os.path.join(root_directory, file_name + ".obj"), vlist, tlist)


if __name__=="__main__":
    run()
