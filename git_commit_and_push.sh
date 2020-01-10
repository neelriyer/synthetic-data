#!/bin/bash
find . -size +30MB | cat >> .gitignore
git add . && git commit -m "new data added" && git push 
