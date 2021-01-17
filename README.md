# Devops Challenge Execution Steps
Steps to build, Run and validate the Solution

Step 1 - Open Bash Terminal to access the git repo
Step 2 - git clone https://github.com/GaganSinghSaluja/devops-challenge-1.git

PLEASE Note - Application will not show any output if the folder location is not update in 		 docker-compose.yaml as stated in step 3

Step 3 - Open docker-compose.yaml file and update the product-service - volumes - source tag to sample Folder path on your machine. 
		 This will map you local file directory to directory on the container.
		 Example change "/c/sample-data" to [Your Folder Location]:/app/data/
		 Sample file can be found under bunningtechapp/data location
		 product-service:    
			depends_on:
			  - db
			networks:
			  - products
			build:
			  context: ./  
			volumes: 
			  - [/c/sample-data]:/app/data 
Step 4 execute command - cd devops-challenge-1/bunningtechapp/
Step 5 execute command - docker-compose build
Step 6 execute command - docker-compose up -d
Step 7 execute command - docker ps -a (view all the running containers)
Step 8 Copy the Container ID for bunningtechapp_product-service_1
Step 9 execute command - docker logs [Container ID] 
Step 10 View the Console app out put

Steps to test if a file is already is processed

Step 11 repeat step 7 to get the [Container ID]
Step 12 docker start [Container ID]
Step 13 docker logs [Container ID]  - Note might have to repeat this step as there is a30 sec wait

#Final Output should look like this
$ docker logs 123f6448e324
wait-for-it.sh: waiting 30 seconds for db:8002
wait-for-it.sh: timeout occurred after waiting 30 seconds for db:8002
Hello from Docker Compose
Processing drop-1.json
Completed  drop-1.json
Power Tools - Artarmon - 20
Power Tools - Notting Hill - 44
Power Tools - Notting Hill - 44
Power Tools - Oakleigh - 7
Processing drop-2.json
Discarding  drop-2.json, incorrect qtysum
Power Tools - Artarmon - 20
Power Tools - Notting Hill - 44
Power Tools - Notting Hill - 44
Power Tools - Oakleigh - 7
Processing drop-3.json
Completed  drop-3.json
Power Tools - Artarmon - 21
Power Tools - Notting Hill - 44
Power Tools - Notting Hill - 44
Power Tools - Oakleigh - 12
 Tiles  - Oakleigh - 1
wait-for-it.sh: waiting 30 seconds for db:8002
wait-for-it.sh: timeout occurred after waiting 30 seconds for db:8002
Hello from Docker Compose
Processing drop-1.json
Skipped   drop-1.json
        

# Known Issues
Issue 1: There was an issues install netcat on the container to execute wait-for-it.sh file. wait-for-it.sh makes a repeated request to the server on a specific host
Quick Fix : Set the default wait time for 30 sec for Mysql server to be ready to take connection request.

