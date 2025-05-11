#!/bin/sh

eval $(minikube -p minikube docker-env)

echo "Deletando a imagem da API"
docker rmi fiap/healthmed-api:1.0.0

echo "Deletando a imagem do Worker"
docker rmi fiap/healthmed-worker:1.0.0
