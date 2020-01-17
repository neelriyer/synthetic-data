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
import csv


df = pd.read_csv('dataset.csv')

# bottom left
df['bottom_left_x'] = df['x_bottom_left'].apply(lambda x: [x])
df['bottom_left_y'] = df['y_bottom_left'].apply(lambda x: [x])

# top right
df['top_right_x'] = df['x_top_right'].apply(lambda x: [x])
df['top_right_y'] = df['y_top_right'].apply(lambda x: [x])

# top left
df['top_left_x'] = df['x_top_left'].apply(lambda x: [x])
df['top_left_y'] = df['y_top_left'].apply(lambda x: [x])

# bottom right
df['bottom_right_x'] = df['x_bottom_right'].apply(lambda x: [x])
df['bottom_right_y'] = df['y_bottom_right'].apply(lambda x: [x])


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
			  
					"Bay": [{} for i in range(len(bottom_left_x))]
			  }

		#result['Bay'][0]['geometry'] = []

		for i in range(len(bottom_left_x)):

			# get values
			bl_x_val = int(bottom_left_x[i])
			bl_y_val = int(bottom_left_y[i])

			tr_x_val = int(top_right_x[i])
			tr_y_val = int(top_right_y[i])

			br_x_val = int(bottom_right_x[i])
			br_y_val = int(bottom_right_y[i])

			tl_x_val = int(top_left_x[i])
			tl_y_val = int(top_left_y[i])

			result['Bay'][i]['geometry'] = [\

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

			#print(result)

		# convert to string
		#result = str(result)

		# replace ' with "
		#result = result.replace("'", '"')

	
		# return json.dumps(result)

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



# convert to json
df['Label'] = df['Label'].apply(lambda x: x if x =='Skip' else json.dumps(x))

# make new cols
df['ID'] = 'test'
df['ID'] = df['ID'].apply(lambda text: uuid.uuid4())
df['filename'] = df.index
#df['External ID'] = df['filename'].apply(lambda text: text.split('.txt')[0] if 'txt' in text else text)
df['External ID'] = df['filename']


# drop cols
select_cols = ['ID', 'Label', 'External ID']
df = df[select_cols]

print(df.columns)


# remove csv if file exists
if os.path.exists('dataset_with_json_column_and_id_added.csv'):
	os.remove('dataset_with_json_column_and_id_added.csv')

# write to csv
print(df['Label'].head(50))
#df.to_csv('dataset_with_json_column_and_id_added.csv', header=True, index=None, sep=',', mode='a', quoting = csv.QUOTE_NONE,escapechar='\\')
df.to_csv('dataset_with_json_column_and_id_added.csv', header=True, index=None, sep=',', mode='a')

check = df[df['Label']!='Skip']
print(len(check))


