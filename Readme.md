## FolderFlatter


FolderFlatter is a command line tool that allows you to copy all the files with a certain extension or extensions recursively 
from a folder hierarchy into an output directory. This tool was born when I had to cope with an intricate folder structure 
trying to find all the images inside it to copy them to another folder to manipulate them.

- - - 

## Usage


This program has three mandatory parameters:

**-r** or **--rootPath**

Tells the root path that we want to scan for files.

**-o** or **--outputPath**

Tells the directory we want to copy the found files to.

**-e** or **--extensions**

Tells the program the extensions we are trying to find, separated by commas.

**-v** or **--overwrite**

Use if you want the program to overwrite the files in the output folder, instead of giving consecutive names. (Optional)

**-m** or **--empty**

Use if you want to delete all the files in the output folder before proceeding. Use with caution. (Optional)

## Examples

``FolderFlatter -r c:/files -o c:/images -e bmp,jpg,gif``

Will search recursively for all the files in c:/files and copy the ones with the extensions bmp, 
jpg or gif to c:/images, without overwriting if there are many files with the same name in the origin folder.

``FolderFlatter -r c:/files -o c:/images -e bmp,jpg,gif -m``

Will do the same as before, but emptying the output folder before starting to copy any file.

``FolderFlatter -r c:/files -o c:/images -e bmp,jpg,gif -v``

Will do the same as before, but overwriting if the file has the same name as something that is already in the 
output folder.

``FolderFlatter -r c:/files -o c:/images -e bmp,jpg,gif -m -v``

Will combine the two parameters explained before.

## License

MIT License

Copyright (c) 2016 David Gil de Gómez Pérez

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
