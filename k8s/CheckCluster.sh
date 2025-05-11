#!/bin/sh

NAMESPACE="fiap-healthmed"

echo "Verificando status dos pods no namespace: $NAMESPACE..."
kubectl get pods -n $NAMESPACE

echo ""
echo "Listando serviços e suas portas:"
kubectl get svc -n $NAMESPACE

echo ""
echo "Usando localhost para URLs acessíveis no navegador"
echo ""

API_PORT=$(kubectl get svc healthmed-api-service -n $NAMESPACE -o jsonpath="{.spec.ports[0].nodePort}")
echo "API Swagger:       http://127.0.0.1:$API_PORT/swagger/index.html"

RABBIT_PORT=$(kubectl get svc healthmed-rabbitmq -n $NAMESPACE -o jsonpath="{.spec.ports[?(@.port==15672)].nodePort}")
echo "RabbitMQ UI:       http://127.0.0.1:$RABBIT_PORT"

MYSQL_PORT=$(kubectl get svc healthmed-mysql -n $NAMESPACE -o jsonpath="{.spec.ports[?(@.port==3306)].nodePort}")
echo "MySQL conexão:     127.0.0.1:$MYSQL_PORT"

echo ""
echo "Verificando pods com erro:"
kubectl get pods -n $NAMESPACE | grep -E 'CrashLoopBackOff|Error|Pending' || echo "✅ Nenhum problema encontrado nos pods."

