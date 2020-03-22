# This tool is designed for easy mesh(.obj) generation

import os
import numpy as np

root_directory = os.path.curdir

# List of all parameters:

separate_file = True
is_manifold = True
thickness = 0.01

file_name = "crane"
x = (np.tan(3.0/16.0*np.pi)-np.tan(np.pi/8.0))/(1+np.tan(3.0/16.0*np.pi))
y = 0.5*(1-np.tan(np.pi/8.0))
z = 1-np.tan(3.0/16.0*np.pi)
m = 2*(1.0/((1.0/np.tan(np.pi/16.0))+(1.0/np.tan(np.pi/8.0))))
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
         (-x, 1-x-np.tan(np.pi / 8.0)),  # J
         (0, 1 - np.tan(np.pi / 8.0)),  # L
         (0, 1 - np.tan(np.pi / 16.0)),  # M
         (0, 1),  # N
         (-1, -1),  # O
         (m-1, m/(np.tan(np.pi/8.0))-1),  # P
         (np.tan(np.pi/8.0) - 1 + x, -x),  # Q
         (-y, -y),  # R
         (-x, -1+x+np.tan(np.pi/8.0)),  # S
         (m/(np.tan(np.pi/8.0))-1, m-1),  # T
         (0, -z/2.0),  # U
         (0, np.tan(np.pi/8.0)-1),  # V
         (0, np.tan(np.pi/16.0)-1),  # W
         (0, -1),  # X
         (1, -1),  # A'
         (1, 0),  # B'
         (-np.tan(np.pi/16.0)+1, 0),  # C'
         (-np.tan(np.pi/8.0)+1, 0),  # D'
         (-np.tan(np.pi/8.0)+1-x, -x),  # E'
         (y, -y),  # F'
         (0, 0),  # G'
         (z/4.0, -z/4.0),  # H'
         (z/2.0, 0),  # I'
         (0, -z/2.0),  # K'
         (x, -1+x+np.tan(np.pi / 8.0)),  # J'
         (0, -1+np.tan(np.pi / 8.0)),  # L'
         (0, -1+np.tan(np.pi / 16.0)),  # M'
         (0, -1),  # N'
         (1, 1),  # O'
         (-m+1, -m/(np.tan(np.pi/8.0))+1),  # P
         (-np.tan(np.pi/8.0)+1-x, x),  # Q'
         (y, y),  # R'
         (x, 1-x-np.tan(np.pi/8.0)),  # S'
         (-m/(np.tan(np.pi/8.0))+1, -m+1),  # T'
         (0, z / 2.0),  # U'
         (0, -np.tan(np.pi / 8.0)+1),  # V'
         (0, -np.tan(np.pi / 16.0)+1),  # W'
         (0, 1)]  # X'
tlist = [(0, 1, 2),
         (0, 2, 3),
         (0, 3, 4),
         (0, 4, 5),
         (0, 5, 10),
         (0, 10, 11),
         (0, 11, 12),
         (0, 12, 13),
         (4, 3, 8),
         (5, 4, 8),
         (5, 8, 7),
         (7, 8, 6),
         (9, 7, 6),
         (10, 5, 7),
         (10, 7, 9),
         (11, 10, 9),
         (1, 14, 2),
         (2, 14, 15),
         (2, 15, 3),
         (3, 15, 16),
         (3, 16, 8),
         (15, 14, 16),
         (16, 14, 17),
         (8, 16, 6),
         (6, 16, 17),
         (6, 17, 18),
         (6, 18, 20),
         (20, 18, 21),
         (17, 14, 18),
         (18, 14, 19),
         (18, 19, 21),
         (20, 18, 21),
         (21, 19, 22),
         (19, 14, 22),
         (22, 14, 23)
         ]  # order for the vertices should be anti-clockwise for
tlist_2 = [(x[0]+24, x[1]+24, x[2]+24) for x in tlist]
tlist += tlist_2
scale = 1


def generate(file_name, vlist, tlist):
    with open(file_name, "w") as fptr:
        fptr.write("# File: '" + file_name + "'\n")
        fptr.write("o " + (file_name.split("/")[-1]).split(".")[0] + "\n")
        for v in vlist:
            fptr.write("v {} {} 0.0".format(v[0] * scale, v[1] * scale) + "\n")
        for t in tlist:
            fptr.write("f {} {} {}".format(t[0], t[1], t[2]) + "\n")


def run():
    if separate_file:
        for i in range(len(tlist)):
            triangle = tlist[i]
            vlist_separate = [vlist[triangle[0]], vlist[triangle[1]], vlist[triangle[2]]]
            generate(os.path.join(root_directory, file_name+f'{i:03}'+".obj"), vlist_separate, [(1, 2, 3)])
    else:
        generate(os.path.join(root_directory, file_name + ".obj"), vlist, tlist)


if __name__=="__main__":
    run()
