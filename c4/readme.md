# C4Model

This model contains the desired diagram of the backend.

## Local setup

Download Structurizr lite for running within a local docker  
`docker pull structurizr/lite`

Starting the c4 model  
`docker run -it --rm -p 8080:8080 -v "${pwd}:/usr/local/structurizr" structurizr/lite`

Open
`http://localhost:8080/`