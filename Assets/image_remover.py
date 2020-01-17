
import os

img_to_remove = ['screen_768x768_269', 'screen_768x768_233', \
				'screen_768x768_283', 'screen_768x768_284', \
				'screen_768x768_269', 'screen_768x768_224',\
				'screen_768x768_227', 'screen_768x768_284', \
				'screen_768x768_283', 'screen_768x768_223', \
				'screen_768x768_225', 'screen_768x768_226', \
				'screen_768x768_232','screen_768x768_270', \
				'screen_768x768_271', 'screen_768x768_278', \
				'screen_768x768_279', 'screen_768x768_280', \
				'screen_768x768_281']


for file in img_to_remove:

	# remove screenshot
	if os.path.exists(os.getcwd()+'/screenshots/'+file+'.png'):
		os.remove(os.getcwd()+'/screenshots/'+file+'.png')

	# remove screenshot
	elif os.path.exists(os.getcwd()+'/screenshots/'+'night_'+file+'.png'):
		os.remove(os.getcwd()+'/screenshots/'+'night_'+file+'.png')


	# remove data txt
	if os.path.exists(os.getcwd()+'/data/'+file+'.png.txt'):
		os.remove(os.getcwd()+'/data/'+file+'.png.txt')

	# remove data txt
	elif os.path.exists(os.getcwd()+'/data/'+'night_'+file+'.png.txt'):
		os.remove(os.getcwd()+'/data/'+'night_'+file+'.png.txt')



