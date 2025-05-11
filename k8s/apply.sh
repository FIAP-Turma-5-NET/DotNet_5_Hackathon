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

echo "Aplicando Prometheus configmap"
kubectl apply -f Monitoring//prometheus-configmap.yaml
sleep 1

echo "Aplicando Prometheus deployment"
kubectl apply -f Monitoring//prometheus-deployment.yaml
sleep 1

echo "Aplicando Prometheus service"
kubectl apply -f Monitoring//prometheus-service.yaml
sleep 1

echo "Aplicando Grafana configmap"
kubectl apply -f Monitoring//grafana-configmap.yaml
sleep 1

echo "Aplicando Grafana deployment"
kubectl apply -f Monitoring//grafana-deployment.yaml
sleep 1

echo "Aplicando Grafana service"
kubectl apply -f Monitoring//grafana-service.yaml
sleep 1

echo "Todos os recursos foram aplicados com sucesso!"

