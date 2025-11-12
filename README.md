# DockVerse

Este projeto consiste numa aplicação web desenvolvida em ASP.NET MVC, com uma base de dados MariaDB, configurada para correr em ambiente Docker.<br>
O objetivo é demonstrar uma estrutura completa e modular, onde cada componente é executado em contentores distintos, facilitando o desenvolvimento, a portabilidade e o controlo de versões.<br>
O projeto foi pensado como uma base para futuros desenvolvimentos web em .NET, permitindo compreender como integrar serviços e automatizar o ambiente de execução através de Docker Compose.

Aplicação usa as seguintes ferramentas:

- Debian - 13.1.0;
- VirtualBox - 7.2.4;
- VMware - 17.6.3;
- Visual Studio 2022 - com .Net 8.0 e ASP.Net;
- MobaXterm 25.3;
- Docker ou Podman;
- Docker Compose ou Podman-Compose;

- Usando uma Virtual Machine (VM):
    - Rede Bridged Adapter
    - `Vbox Guest Additions` ou `VMWare Tools` instalado
    - Os pacotes `docker.io` e `docker-compose` **OU** `podman` e `podman-compose` instalados
    - (Opcional) Cliente de SSH/SFTP `MobaXterm`, para acessar a VM

- Usando Docker Desktop (Ambiente Windows/WSL):
    - Docker Desktop

## Como configurar o projeto

Para fazer o setup do projeto, **recomenda-se** a configuração de uma máquina virtual Debian com [pasta partilhada](Docs/pastaPartilhada.md).

No entanto, também é possível recrear o ambiente de desenvolvimento em Windows, através do Docker Desktop. Para tal, clone o repositório num diretório da sua escolha e siga os seguintes passos para correr o projeto.

## Utilização
Para correr o projeto abra um terminal no diretório `dockerProject\dockerProject` do repositório, e corra:

- No caso de usar `docker`:
    ```bash
    docker compose up --build -d
    ```
- No caso de usar `podman`:
    ```bash
    podman-compose up --build -d
    ```

## Testar no Browser

Após o build:

- A aplicação estará disponível em:  
    
 **http://IP_DO_DEBIAN:8080**

---

## Comandos úteis

| Descrição | Comando |
|------------|----------|
| Build e iniciar containers | `docker compose up --build -d` |
| Parar containers | `docker compose down` |
| Ver containers ativos | `docker ps` |
| Ver logs da app | `docker logs aspnetapp --tail=50` |
| Entrar no container da DB | `docker exec -it mariadb mariadb -u root -p` |
| Ver dados | `USE meusite; SELECT * FROM students;` |

---

## Resultado Final

Após seguir estes passos, terás:
- Um container **MariaDB** com dados persistentes.  
- Um container **ASP.NET Core** totalmente funcional.  
- Comunicação automática entre ambos através do Compose.  
- Deploy simples com um único comando:  
  ```bash
  docker compose up --build -d
  ```
