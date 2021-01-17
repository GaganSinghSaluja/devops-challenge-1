# Devops Challenge Execution Steps
Steps to build, Run and validate the Solution

Step 1 - Open Bash Terminal to access the git repo
Step 2 - git clone https://github.com/GaganSinghSaluja/devops-challenge-1.git
Step 3 - Open docker-compose.yaml file and update the product-service - volumes - source tag to sample Folder path on your machine. 
		 This will map you local file directory to directory on the container.
		 Example change "/c/sample-data" to [Your Folder Location]:/app/data/
		 Default sample file location bunningtechapp/data
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

        

# Known Issues
Issue 1: There was an issues install netcat on the container to execute wait-for-it.sh file. wait-for-it.sh makes a repeated request to the server on a specific host
Quick Fix : Set the default wait time for 30 sec for Mysql server to be ready to take connection request.

