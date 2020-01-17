import os

# function to get list of files in directory
def list_of_files(mypath):
	from os import listdir
	from os.path import isfile, join
	files = [f for f in listdir(mypath) if (isfile(join(mypath, f)) and (f.endswith(".png.txt") or f.endswith(".png")))]

	return files

files = list_of_files(os.getcwd()+'/screenshots')
for file in files:

	print(file)
	path = os.getcwd()+'/screenshots/'+file
	new_path = os.getcwd()+'/screenshots/'+'night_'+file

	os.rename(path, new_path)

files = list_of_files(os.getcwd()+'/data')
for file in files:

	print(file)
	path = os.getcwd()+'/data/'+file
	new_path = os.getcwd()+'/data/'+'night_'+file
	
	os.rename(path, new_path)


	