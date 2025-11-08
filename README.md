Uma breve introdução ao projeto...

# Como Configurar o Projeto

Para configurar o projeto, são necessárias as seguintes dependências:

## Dependências Necessárias

- Debian - 13.1.0;
- VirtualBox - 7.2.4;
- VMware - 17.6.3;
- Visual Studio 2022 - com .Net 8.0 e ASP.Net;
- MobaXterm 25.3;
- Docker;
- Docker Compose;

## Configurar Pasta Partilhada

Esta secção demostra como configurar uma pasta partilhada entre o Windows (host) e uma máquina virtual Debian, de modo a facilitar o desenvolvimento no Visual Studio 2022.

### VirtualBox

Para configurar uma pasta partilhada entre o Windows host e a máquina virtual Linux:

1. Crie uma pasta no ambiente de trabalho do Windows host.

![[Captura de ecrã 2025-11-07 175821.png]]

2. Abra o VirtualBox, selecione a máquina virtual Debian, e aceda às Definições e Pastas Partilhadas.

![[Captura de ecrã 2025-11-07 180006.png]]

3. Dentro do menu Pastas Partilhadas, clique em Pasta da Máquina e no ícone da pasta com o símbolo "+" verde:
	1. No menu Editar Partilha, escolha a pasta a partilhar.
	2. O Nome da Pasta será atribuído automaticamente.
	3. Em Ponto de Montagem (Mount Point), indique o caminho onde será montada a pasta no Linux.
	4. Ative a opção Montar Automaticamente para que a partilha seja montada em cada arranque.

![[Captura de ecrã 2025-11-07 180139.png]]

4. Inicie a máquina Debian e entre como utilizador "root".
5. No menu Dispositivos, selecione Inserir imagem de CD dos Guest Additions.

![[Captura de ecrã 2025-11-07 180304.png]]

6. Crie as pastas necessárias para a montagem.

![[Captura de ecrã 2025-11-07 180942.png]]

7. Monte o CD das Guest Additions com o comando:

```Bash
mount /dev/cdrom /mnt/cdrom
```

![[Captura de ecrã 2025-11-07 181039.png]]

8. Verifique o conteúdo da pasta montada:

```Bash
ls -la /mnt/cdrom
```

![[Captura de ecrã 2025-11-07 181053.png]]

9. Atualize os repositórios e o sistemas:

```Bash
apt update && apt upgrade -y
```

![[Captura de ecrã 2025-11-07 181624.png]]

10. Instale os pacotes necessários:

```Bash
apt install build-essential dkms linux-headers-$(uname -r) -y
```

- **Build-essential**: Metapacote que instala ferramentas básicas de compilação no Linux.
- **dkms**: **Dynamic Kernel Module Support**, recompila automaticamente módulos de kernel (como drivers) após atualizações.
- linux-headers: Ficheiros de cabeçalho necessários para automaticamente módulos compatíveis com o kernel.
- $(uname -r): Retorna a versão exata do kernel em execução.

![[Captura de ecrã 2025-11-07 181955.png]]

![[Captura de ecrã 2025-11-07 182028.png]]

11. Reinicie a máquina para aplicar todas as alterações:

```Bash
reboot
```

![[Captura de ecrã 2025-11-08 143452.png]]

12. Após o arranque, confirme o conteúdo da pasta partilhada:

![[Captura de ecrã 2025-11-08 143511.png]]

### VVWARE

Para configurar uma pasta partilhada no VMware entre o Windows host e a máquina virtual Linux:

1. Crie uma pasta no ambiente de trabalho do Windows host.

![[Captura de ecrã 2025-11-07 175821.png]]

2. Com a máquina virtual desligada, selecione Edit Virtual Machine Settings.

![[Pasted image 20251107174312.png]]

3. Aceda a Options e Shared Folders, e selecione Always Settings.

![[Pasted image 20251107174538.png]]

4. Clique em Add para escolher a pasta a partilhar.

![[Pasted image 20251107174723.png]]

5. Confirme que a pasta aparece listada com a caixa de seleção ativa.

![[Pasted image 20251107174932.png]]

6. Verifique se pacote "open-vm-tools" está instalado (por defeito, costuma vir pré-instalado):

```Bash
vmhgfs-fuse --version
```

```Bash
sudo apt update
sudo apt install open-vm-tools -y
```

7. Crie o diretório de montagem:

```bash
sudo mkdir /mnt/SharedDir
```

8. Edite o ficheiro "/etc/fstab" e adicione a seguinte linha:

```
.host:/         /mnt/SharedDir  fuse.vmhgfs-fuse        defaults,allow_other    0       0
```

9. Guarde e monte o volume:

```bash
sudo systemctl daemon-reload
sudo mount -a
```

10. Confirme que o diretório foi montado corretamente:

```bash
ls /mnt/SharedDir/
```

## Titulo de quem vem a seguir