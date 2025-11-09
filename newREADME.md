# Project Title

Aplicação Web ASP.NET MVC com MariaDB
## Tabela de Conteúdos
- [Sobre](#sobre)
- [Instalação](#instalar)
- [Utilização](#utilização)
- [Documentation](#documentation)
- [Contributing](#contributing)
- [License](#license)

## Sobre
Este projeto consiste numa aplicação web desenvolvida em ASP.NET MVC, com uma base de dados MariaDB, configurada para correr em ambiente Docker.
O objetivo é demonstrar uma estrutura completa e modular, onde cada componente é executado em contentores distintos, facilitando o desenvolvimento, a portabilidade e o controlo de versões.

O projeto foi pensado como uma base para futuros desenvolvimentos web em .NET, permitindo compreender como integrar serviços e automatizar o ambiente de execução através de Docker Compose.

### Dependências

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

## Instalar
Para fazer o setup do projeto, **recomenda-se** a [configuração de uma VM Debian](docs/vm.md)

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

## 📄 License