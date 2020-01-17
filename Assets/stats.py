def list_of_files(mypath):
	from os import listdir
	from os.path import isfile, join
	files = [f for f in listdir(mypath) if (isfile(join(mypath, f)) and f.endswith(".png.txt"))]

	return files

files = list_of_files('/screenshots')
print(files)
print(len(files))