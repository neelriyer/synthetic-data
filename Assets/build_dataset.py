import pandas as pd
import os
import numpy as np

SIZE_WIDTH = 768
SIZE_HEIGHT = 768

#function to get list of files in directory
def list_of_files(mypath):
	from os import listdir
	from os.path import isfile, join
	files = [f for f in listdir(mypath) if (isfile(join(mypath, f)) and 'meta' not in f)]

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

	# merge on ekey
	merged = bottom_left.merge(top_right, left_on='key', right_on='key', suffixes=('_bottom_left', '_top_right'))
	#print(merged)

	# add class
	merged['class'] = 'parkinglot'

	# rename cols
	merged.rename(columns={"filename_bottom_left": "filename", 
		"x_bottom_left": "x_start", "y_bottom_left": "y_start", 
		"x_top_right": "x_finish", "y_top_right": "y_finish"}, inplace = True)

	# get req cols
	merged = merged[['filename', 'class', 'x_start', 'y_start', 'x_finish', 'y_finish']]

	return merged


# get list of files in /data dir
files = list_of_files(os.getcwd()+'/data')
#print(files)

# create empty df
df = pd.DataFrame()

# iterate through all files
for file in files:

	# get df for txt file
	df2 = convert_txt_to_pandas(file)

	#print(file)
	#print(df2.head())

	#if empty
	if(df2.empty):
		df2 = pd.DataFrame({
			'filename': file, 
			'class': 'parkinglot', 
			'x_start': np.nan,
			'y_start': np.nan,
			'x_finish': np.nan,
			'y_finish': np.nan}, index=[0])
		#print(df2.head())
		#print('here')

	# append to final df
	df = df.append(df2, ignore_index = True)


# remove csv if file exists
if os.path.exists(os.getcwd()+'/dataset.csv'):
	os.remove(os.getcwd()+'/dataset.csv')

# write to csv
df.to_csv(os.getcwd()+'/dataset.csv', header=True, index=None, sep=',', mode='a')


