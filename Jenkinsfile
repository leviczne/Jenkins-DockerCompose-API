pipeline{
    agent any
    stages {
        stage ("verfify tooling"){
            steps{
                bat '''
                docker version
                docker info
                docker compose version
                curl --version
                '''
            }
        }
    }

}