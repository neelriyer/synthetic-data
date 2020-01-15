"""
script to change dataset into labelbox format

{
	"geometry": [
		{
			"x": 635,
			"y": 224
		},
		{
			"x": 557,
			"y": 223
		},
		{
			"x": 554,
			"y": 257
		},
		{
			"x": 635,
			"y": 264
		}
	]
}

"""

#import subprocess
#subprocess.run(['python', 'build_dataset.py'])
import pandas as pd 
import json
import math
import uuid
import os


df = pd.read_csv('dataset.csv')

# bottom left
df['bottom_left_x'] = df['x_start'].apply(lambda x: [x])
df['bottom_left_y'] = df['y_start'].apply(lambda x: [x])

# top right
df['top_right_x'] = df['x_finish'].apply(lambda x: [x])
df['top_right_y'] = df['y_finish'].apply(lambda x: [x])

# top left
df['top_left_x'] = df['x_start'].apply(lambda x: [x])
df['top_left_y'] = df['y_finish'].apply(lambda x: [x])

# bottom right
df['bottom_right_x'] = df['x_finish'].apply(lambda x: [x])
df['bottom_right_y'] = df['y_start'].apply(lambda x: [x])


# grouped_df=df.groupby('filename',as_index=False).agg({'x_start':','.join,'y_start':','.join})

# aggregate by filename
df = df.groupby('filename').agg({'bottom_left_x': 'sum', \
										'bottom_left_y': 'sum',\
										'top_right_x': 'sum',\
										'top_right_y': 'sum',\
										'top_left_x': 'sum',\
										'top_left_y': 'sum',\
										'bottom_right_x': 'sum',\
										'bottom_right_y': 'sum',\
										})



def geometry_calculator(bottom_left_x, bottom_left_y, top_right_x, top_right_y, bottom_right_y, bottom_right_x, top_left_y, top_left_x):

	# sanity check
	#if(len(bottom_left_x)!=len(top_right_y) or )

	result = 'Skip'

	if(not math.isnan(bottom_left_x[0])):

		result = {\
			  
					"Bay": [
			  	
				  		{
				  			'geometry': []
				  		}

			  		]
			  }

		#result['Bay'][0]['geometry'] = []

		for i in range(len(bottom_left_x)):

			# get values
			bl_x_val = str(bottom_left_x[i])
			bl_y_val = str(bottom_left_y[i])

			tr_x_val = str(top_right_x[i])
			tr_y_val = str(top_right_y[i])

			br_x_val = str(bottom_right_x[i])
			br_y_val = str(bottom_right_y[i])

			tl_x_val = str(top_left_x[i])
			tl_y_val = str(top_left_y[i])

			result['Bay'][0]['geometry'] = \
					[\

						{\
						  "x": bl_x_val,\
						  "y": bl_y_val \
						},\
						{\
						  "x": tl_x_val,\
						  "y": tl_y_val\
						},\
						{\
						  "x": tr_x_val,\
						  "y": tr_y_val\
						},\
						{\
						  "x": br_x_val,\
						  "y": br_y_val\
						}\

					]\

			print(result)

	return result


# df.apply(lambda row: row['bottom_left_x'], axis = 1)


df['Label'] = df.apply(lambda row: geometry_calculator(bottom_left_x = row['bottom_left_x'],\
														bottom_left_y = row['bottom_left_y'],\
														top_right_x = row['top_right_x'],\
														top_right_y = row['top_right_y'],\
														bottom_right_y = row['bottom_right_y'],\
														bottom_right_x = row['bottom_right_x'],\
														top_left_y = row['top_left_y'],\
														top_left_x = row['top_left_x']), axis = 1)

# make new cols
df['ID'] = 'test'
df['ID'] = df['ID'].apply(lambda text: uuid.uuid4())
df['filename'] = df.index
df['External ID'] = df['filename']

# drop cols
select_cols = ['ID', 'Label', 'External ID']
df = df[select_cols]

print(df.columns)
print(df['Label'].value_counts())
print(df.head())

# remove csv if file exists
if os.path.exists('dataset_with_json_column_and_id_added.csv'):
	os.remove('dataset_with_json_column_and_id_added.csv')

# write to csv
df.to_csv('dataset_with_json_column_and_id_added.csv', header=True, index=None, sep=',', mode='a')


