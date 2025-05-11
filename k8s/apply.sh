#!/bin/bash

echo ">> Aplicando namespace"
kubectl apply -f namespace.yaml

echo ">> Aplicando ConfigMap"
kubectl apply -f configmap.yaml

echo ">> Aplicando Secrets"
kubectl apply -f secrets.yaml

echo ">> Aplicando recursos do Banco de Dados (MySQL)"
kubectl apply -f DataBase/

echo ">> Aplicando recursos de Monitoramento (RabbitMQ)"
kubectl apply -f Monitoring/

echo ">> Aplicando Service da API"
kubectl apply -f Application/api-service.yaml

echo ">> Aplicando Deployments da Aplicação (API e Worker)"
kubectl apply -f Application/api-deployment.yaml
kubectl apply -f Application/worker-deployment.yaml

echo ">> Aplicando HPAs da Aplicação (API e Worker)"
kubectl apply -f Application/hpa-api.yaml
kubectl apply -f Application/hpa-worker.yaml

echo "Todos os recursos foram aplicados com sucesso!"

