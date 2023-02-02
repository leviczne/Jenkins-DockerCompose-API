
pipeline { 

  // This pipeline requires the following plugins: 

  // * Git: https://plugins.jenkins.io/git/ 

  // * Workflow Aggregator: https://plugins.jenkins.io/workflow-aggregator/ 

  // * MSTest: https://plugins.jenkins.io/mstest/ 

  agent 'any' 

  stages { 

    stage('Environment') { 

      steps { 

          echo "PATH = ${PATH}" 

      } 

    } 

    stage('Checkout') { 

      steps { 

  

        script { 

            checkout([$class: 'GitSCM', branches: [[name: '*/mainb']], userRemoteConfigs: [[url: 'https://github.com/leviczne/sdsds.git']]]) 

        } 

      } 

    } 

    stage('Dependencies') { 

      steps { 

          script{ 

            if (isUnix()){  

                sh(script: 'dotnet restore') 

                 

            } 

            else { 

                bat (script: 'dotnet restore ./SistemaDeCadastroAPI/SistemaDeCadastroAPI.csproj')

                 

            } 

          } 

      } 

    } 

    stage('Build') { 

      steps { 

          script{ 

              if (isUnix()){ 

                sh(script: 'dotnet build --configuration Release', returnStdout: true) 

              } 

              else{ 

                  bat(script: 'dotnet build ./SistemaDeCadastroAPI/SistemaDeCadastroAPI.csproj --configuration Release', returnStdout: true) 

              } 

          } 

      } 

    } 

    stage('Test') { 

      steps { 

          script{ 

               if (isUnix()){ 

                sh(script: 'dotnet test ./SistemaDeCadastroAPI/SistemaDeCadastroAPI.csproj -l:trx')   

               } 

               else { 

                   bat (script: 'dotnet test ./SistemaDeCadastroAPI/SistemaDeCadastroAPI.csproj -l:trx')   

               } 

          } 

      } 

    } 
    
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

  post { 

    always { 

      mstest(testResultsFile: '**/*.trx', failOnError: false, keepLongStdio: true) 

    } 

  } 
  

} 