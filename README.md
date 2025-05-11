# 🩺 FIAP HealthMed

Sistema distribuído baseado em microsserviços com API, Worker, banco MySQL e mensageria via RabbitMQ. Esta aplicação foi construída utilizando Docker e Docker Compose para facilitar a execução local.

## 📦 Estrutura

- `api` - API principal (.NET 8)
- `worker` - Serviço de background para processamento de mensagens
- `mysql` - Banco de dados relacional
- `rabbitmq` - Broker de mensageria

---

## 🚀 Como rodar o projeto

### Pré-requisitos
- Docker instalado [https://docs.docker.com/get-docker/](https://docs.docker.com/get-docker/)
- Docker Compose instalado (geralmente já incluso com o Docker Desktop)

### 1. Clone o repositório

```bash
git clone https://github.com/seu-usuario/fiap-healthmed.git
cd fiap-healthmed
```

### 2. Suba os containers com Docker Compose

```bash
docker-compose up --build -d
```

Esse comando:
- Constrói a API e o Worker a partir do `Dockerfile`
- Sobe o banco MySQL com scripts de inicialização
- Sobe o RabbitMQ com painel administrativo
- Expõe as portas para uso local

---

## 🔗 Acessos rápidos

- 🧪 **Swagger da API**: [http://localhost:7209/swagger](http://localhost:7209/swagger)
- 🐰 **Painel do RabbitMQ**: [http://localhost:15672](http://localhost:15672)
  - Usuário: `guest`
  - Senha: `guest`
- 🛢️ **MySQL**: Host `localhost`, porta `3360`
  - Usuário: `root`
  - Senha: `202406`
  - Banco: `HealthMed`

---

## 🧪 Como testar

### ✅ 1. Verificar se os containers estão rodando

```bash
docker ps
```

Você deve ver os containers:
- `fiap_healthmed_api`
- `fiap_healthmed-worker`
- `mysql-fiap-HealthMed`
- `rabbitmq`

### ✅ 2. Acessar a API e realizar requisições via Swagger

Exemplo:
- Realize um `POST` para criar um recurso (usuário, consulta, etc.)
- Em seguida, faça um `GET` para verificar se foi persistido

### ✅ 3. Verificar persistência no MySQL

```bash
docker exec -it mysql-fiap-HealthMed mysql -uroot -p202406
```

```sql
USE HealthMed;
SHOW TABLES;
SELECT * FROM NomeDaTabela;
```

### ✅ 4. Verificar envio de mensagens para o RabbitMQ

Acesse o painel RabbitMQ em [http://localhost:15672](http://localhost:15672) e vá em "Queues". As filas `usuario-queue` e `consulta-queue` devem estar criadas e sendo utilizadas.

### ✅ 5. Verificar logs do Worker

```bash
docker logs fiap_healthmed-worker
```

Você verá as mensagens sendo processadas após interações com a API.

---

## 📂 Scripts de banco

Os scripts `.sql` de criação do banco estão em:

```
src/FIAP_HealthMed.Data/Scripts/
```

Eles são executados automaticamente ao subir o container `mysql`.

---

## 🧹 Parar os serviços

```bash
docker-compose down
```

Se quiser remover também os volumes (banco de dados):

```bash
docker-compose down -v
```

---

# Projeto no Kubernetes (Windows)

⚠️ Instalar Minikube: https://minikube.sigs.k8s.io/docs/start/?arch=%2Fwindows%2Fx86-64%2Fstable%2F.exe+download

⚠️Abra :

👉 Abra o Docker desktop e o Visual Code com o código-font.

- No terminar Git Bash entre no caminho que estão o scripts

```console
cd scriptInicial
```

- Após entrar no caminho execute os scripts:

```console
sh MinikubeStart.sh
```

```console
sh CriarImagem.sh
```

- Minikube por Dasboard

```console
minikube dashboard
```

⚠️ Após terminar de subir as imagens

- Em um novo terminal Git Bash entre no caminho que está o K8S.

```console
cd  k8s
```

- Execute o script

```console
sh apply.sh
```

---

## Acessar API, Prometheus e Grafana Kubernetes (Minikube)

👉 No browser acesse a API pela a url: http://localhost:32080/swagger/index.html

👉 No browser acesse a RabbitMQ pela a url: http://localhost:32072/

👉 No browser acesse a Prometheus pela a url: http://localhost:31003/

👉 No browser acesse a Grafana pela a url: http://localhost:31004/

---

## ✍️ Autor

**Seu Nome** - [@seuusuario](https://github.com/seuusuario)
