#!/bin/sh

eval $(minikube -p minikube docker-env)

echo "Criando a imagem da API"
docker build --target api -f ../Dockerfile -t fiap/healthmed-api:1.0.0 ../

echo "Criando a imagem do Worker"
docker build --target worker -f ../Dockerfile -t fiap/healthmed-worker:1.0.0 ../

echo "Imagens criadas com sucesso!"


