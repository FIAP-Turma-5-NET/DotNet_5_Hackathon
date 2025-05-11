# FIAP_HealthMed

<h1 align="center">Turma - 5 .NET - Hackathon</h1>
<h3 align="center">HealthMed</h3>

---

## 📑 Sumário

<!--ts-->

- [Pré-requisitos](#pré-requisitos)
- [Configurar DBeaver](#configurar-dbeaver)
- [Projeto no Kubernetes (Windows)](#projeto-no-kubernetes-windows)
- [Acessar API, Prometheus, Grafana e Kubernetes (Minikube)](#acessar-api-prometheus-e-grafana-kubernetes-minikube)
- [✍️ Autor](#autor)

<!--te-->

---
### Pré-requisitos
- Docker instalado [https://docs.docker.com/get-docker/](https://docs.docker.com/get-docker/)
- Minikube instalado [https://minikube.sigs.k8s.io/docs/start/?arch=%2Fwindows%2Fx86-64%2Fstable%2F.exe+download](https://minikube.sigs.k8s.io/docs/start/?arch=%2Fwindows%2Fx86-64%2Fstable%2F.exe+download)

---

## 🔧 Configurar DBeaver

👉 No DBeaver, acesse o banco MySQL com os seguintes dados:

```text
Servidor: 127.0.0.1
Banco de dados: HealthMed
Porta: 32006
Usuário: root
Senha: root


```

---


# Projeto no Kubernetes (Windows)

⚠️ Instalar Minikube

👉 Abra o Docker desktop e o Visual Code com o código-fonte.

- No terminal Git Bash entre no caminho que estão o scripts

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

- Minikube por Dashboard

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


## Acessar API, Prometheus, Grafana e Kubernetes (Minikube)

👉 No browser acesse a API pela URL: http://localhost:32080/swagger/index.html

👉 No browser acesse a RabbitMQ pela URL: http://localhost:32072/

👉 No browser acesse a Prometheus pela URL: http://localhost:31003/

👉 No browser acesse a Grafana pela URL: http://localhost:31004/

---

## ✍️ Autor

**Seu Nome** - [@seuusuario](https://github.com/seuusuario)