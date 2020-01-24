import pandas as pd
import os
import numpy as np


SIZE_WIDTH = 768
SIZE_HEIGHT = 768


# remove bad images
# import subprocess
# subprocess.run(['python', 'image_remover.py'])


def write_to_csv(df, path):

	# remove csv if file exists
	if os.path.exists(os.getcwd()+'/'+path+'.csv'):
		os.remove(os.getcwd()+'/'+path+'.csv')

	# write to csv
	df.to_csv(os.getcwd()+'/'+path+'.csv', header=True, index=None, sep=',', mode='a')


# function to get list of files in directory
def list_of_files(mypath):
	from os import listdir
	from os.path import isfile, join
	files = [f for f in listdir(mypath) if (isfile(join(mypath, f)) and f.endswith(".png.txt"))]

	return files

def convert_txt_to_pandas(file):

	# read first dataset
	df = pd.read_csv(os.getcwd()+'/data/'+file)

	# set cols
	df.columns = ['filename', 'class', 'x', 'y']

	# set to float
	df['x'] = df['x'].astype(float)
	df['y'] = df['y'].astype(float)

	# filter dataframe
	# we only want x and y values that are within the frame
	df = df[(df['x']<=SIZE_WIDTH) & (df['y']<=SIZE_HEIGHT) & (df['x']>=0) & (df['y']>=0)]

	# reset y pixels
	df['y'] = SIZE_HEIGHT - df['y'] 

	# get key
	df['key'] = df['class'].apply(lambda text: str(text.split()[0][-1]) + ' ' + str(text.split()[-1]))

	# get bottom left and top right
	bottom_left = df[df['class'].str.contains("bottom_left")]
	#print(bottom_left.head())

	top_right = df[df['class'].str.contains("top_right")]
	#print(top_right.head())

	bottom_right = df[df['class'].str.contains("bottom_right")]
	#print(bottom_right.head())

	top_left = df[df['class'].str.contains("top_left")]
	#print(top_left.head())



	# merge bottom left and top right
	merged_bl_tr = bottom_left.merge(top_right, left_on=['key', 'filename'], right_on=['key','filename'], suffixes=('_bottom_left', '_top_right'))

	# merge bottom right and top left
	merged_br_tl = bottom_right.merge(top_left, left_on=['key', 'filename'], right_on=['key','filename'], suffixes=('_bottom_right', '_top_left'))

	# merge everything
	final_merge = merged_br_tl.merge(merged_bl_tr, left_on=['key', 'filename'], right_on=['key','filename'], suffixes=('_br_tl', '_bl_tr'))

	# print(final_merge.head())


	# add class
	final_merge['class'] = 'parkinglot' 

	# get req cols
	final_merge = final_merge[['filename', 'class', 'x_bottom_right', 'y_bottom_right', 'x_top_left', 'y_top_left', 'x_bottom_left', 'y_bottom_left', 'x_top_right', 'y_top_right']]

	return final_merge


# get list of files in /data dir
files = list_of_files(os.getcwd()+'/data')
# print(files)

# create empty df
df = pd.DataFrame()


# testing
#test = convert_txt_to_pandas(files[0])
#convert_txt_to_pandas(files[1])
#convert_txt_to_pandas(files[2])


# iterate through all files
for file in files:

	# get df for txt file
	# print(file)
	df2 = convert_txt_to_pandas(file)

	print(file)
	# print(df2.head())

	# if empty
	if(df2.empty):
		df2 = pd.DataFrame({
			'filename': file.split('.txt')[0], 
			'class': 'parkinglot', 
			'x_bottom_right': np.nan,
			'y_bottom_right': np.nan,
			'x_top_left': np.nan,
			'y_top_left': np.nan,
			'x_bottom_left': np.nan,
			'y_bottom_left': np.nan,
			'x_top_right': np.nan,
			'y_top_right': np.nan}, index=[0])
		# print(df2.head())
		# print('here')

	# append to final df
	df = df.append(df2, ignore_index = True)


# write to csv
write_to_csv(df, 'dataset')

