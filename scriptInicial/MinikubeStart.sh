#!/bin/sh

echo "Verificando se o Docker está rodando..."
if ! docker info > /dev/null 2>&1; then
  echo "Docker não está ativo ou não foi encontrado."
  echo "Certifique-se de que o Docker Desktop está iniciado e com o WSL2 ou Hyper-V ativado."
  echo "Alternativa: use driver Hyper-V com um switch virtual configurado."
  echo "Exemplo: minikube start --driver=hyperv --hyperv-virtual-switch=\"Default Switch\""
  exit 1
fi

echo "Docker detectado com sucesso."

# Define Kubernetes version e portas expostas
K8S_VERSION="v1.30.0"
PORTS="8080:80,32006:32006,32072:32072,32080:32080,31003:31003,31004:31004 "

echo "Iniciando Minikube com Kubernetes $K8S_VERSION usando Docker..."

minikube start \
  --driver=docker \
  --kubernetes-version=$K8S_VERSION \
  --ports=$PORTS \
  --extra-config=kubelet.housekeeping-interval=10s

# Verifica se o cluster subiu corretamente
if [ $? -eq 0 ]; then
  echo "Minikube iniciado com sucesso!"
  echo "Verificando status do cluster..."
  minikube status
else
  echo "Falha ao iniciar o Minikube."
  echo "Verifique se você tem permissões, memória e drivers corretos instalados."
  exit 1
fi
