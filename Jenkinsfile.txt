pipeline{
	agent 'Slave'
  stages{  
    stage('Checkout'){
		checkout scm
		}
    stage('Build'){
		bat 'nuget restore CodedUITestProject1.sln'
		bat "\"${tool 'MSBuild'}\" CodedUITestProject1.sln /p:Configuration=Release /p:Platform=\"Any CPU\" /p:ProductVersion=1.0.0.${env.BUILD_NUMBER}"
    }
	}
 }
	
