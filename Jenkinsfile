// Define an execution pipeline
pipeline {
    // Say that it can use any available agent (Master or Slave)
    agent any

    // Define build stages
    stages {
        stage('Initialize') {
            steps {
                // Enable remote triggers
                script {
                    properties([pipelineTriggers([pollSCM('* * * * *')])])
                }
                // Define scm connection for polling
                git branch: 'main', url: 'https://github.com/Mike-Opanasiuk/TestTask_Wallet.git'
                // Clean all containers
                bat 'docker-compose -f ./WalletApi/docker-compose.yml kill'
                
                // Clean all docker components
                bat 'docker system prune -a --volumes -f'
            }
        }
        stage('Checkout') {
            steps {
                // Checkout the code from the Git repository
                checkout([$class: 'GitSCM', branches: [[name: '*/main']], userRemoteConfigs: [[url: 'https://github.com/Mike-Opanasiuk/TestTask_Wallet.git']]])
            }
        }

        stage('Build') {
            steps {
                // Show files in current directory
                bat 'dir'
                // Run docker-compose
                bat 'docker-compose -f ./WalletApi/docker-compose.yml up -d --wait'
                // Show current running containers
                bat 'docker-compose -f ./WalletApi/docker-compose.yml ps'
            }
        }
    }

    // Define a section that will be executed at the end of the pipeline execution
    post {
        // In case of success
        success {
            // Print that build is successful
            echo 'Build successful!'
        }
        // In case of failure
        failure {
            // Print that build is failed
            echo 'Build failed!'
        }
    }
}
