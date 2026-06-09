# Roteiro do vídeo demonstrativo — DevOps Tools & Cloud Computing

Tempo-alvo: 4 a 7 minutos. Grave a tela da VM (não do localhost). Resolução mínima 720p, áudio limpo, sem cortes que escondam etapas.

Lembre-se do que o professor escreveu: "O vídeo é a evidência da entrega. É a prova da disciplina." Tudo que está na rubrica precisa aparecer rodando **em nuvem**.

---

## 0:00 — Abertura (15s)

> "Olá, somos o grupo da Global Solution 2026/1. Eu sou o Nicolas, RM561857, e este é o vídeo da disciplina de DevOps Tools and Cloud Computing. Nossa solução é a PM_API, uma API REST de monitoramento de datacenters por sensores, conteinerizada com Docker e rodando numa máquina virtual na Azure."

Mostre na tela: o terminal já conectado por SSH na Azure VM. Rode `hostname` e `whoami` para deixar claro que está na nuvem, não no seu computador.

---

## 0:15 — Mostrar o repositório e clonar (40s)

> "Tudo que é necessário para rodar o projeto está no nosso repositório no GitHub. Vou começar exatamente como está no nosso README: clonando o repositório do zero."

```bash
git clone <URL_DO_REPO>
cd <pasta-do-repo>
ls -l
```

> "Aqui temos o Dockerfile da aplicação, o docker-compose.yml que orquestra os dois containers, o código-fonte da API em .NET e o README com o passo a passo."

Abra rapidamente o `docker-compose.yml` na tela e aponte: o serviço do app, o serviço do banco, a rede `pmnet` e o volume `pmdata`.

---

## 0:55 — Subir os containers (40s)

> "Agora subo os dois containers em segundo plano, já construindo a imagem personalizada da aplicação."

```bash
docker compose up -d --build
```

> "O parâmetro -d executa em background, e o --build garante que a imagem do app seja construída a partir do nosso Dockerfile."

Aguarde terminar. Depois:

```bash
docker compose ps
```

> "Os dois containers estão de pé: o app-RM561857, com a API .NET, e o db-RM561857, com o PostgreSQL. Os nomes contêm o RM do representante da equipe, como exige o enunciado."

---

## 1:35 — Mostrar os logs (30s)

> "Conforme pedido, exibo os logs de cada container."

```bash
docker compose logs app-RM561857
docker compose logs db-RM561857
```

> "Nos logs do app vemos a aplicação iniciando e escutando na porta 8080. Nos logs do banco, o PostgreSQL pronto para aceitar conexões."

---

## 2:05 — Acessar os containers por dentro (45s)

> "Agora acesso o terminal de cada container para evidenciar a estrutura de diretórios e o usuário conectado."

```bash
docker container exec -it app-RM561857 sh -c "pwd && ls -l && whoami"
```

> "No container da aplicação, o diretório de trabalho é /app, e o usuário conectado é o pmuser — um usuário não privilegiado, e não o root. Isso atende ao requisito de segurança do enunciado."

```bash
docker container exec -it db-RM561857 sh -c "pwd && ls -l && whoami"
```

> "No container do banco, mesma demonstração: diretório e usuário conectado."

---

## 2:50 — Demonstrar o CRUD pela API (90s)

> "Vou demonstrar o CRUD completo, com os dados sendo gravados via API no banco — nunca apenas no dispositivo."

Abra o navegador em `http://<IP_PUBLICO_DA_VM>:8080/swagger`.

> "Aqui está a documentação Swagger da API, acessível externamente pelo IP público da máquina na Azure."

Demonstre as quatro operações em uma entidade (por exemplo, DataCenter):

1. **CREATE** — POST em `/api/DataCenter` com um corpo de exemplo. Mostre o 201/200 de resposta.
   > "Crio um datacenter. A API retorna o registro criado com o ID gerado."
2. **READ** — GET em `/api/DataCenter`. Mostre o registro na lista.
   > "Listo os datacenters e confirmo que o registro foi persistido."
3. **UPDATE** — PUT em `/api/DataCenter/{id}` alterando um campo.
   > "Atualizo o registro e confirmo a alteração."
4. **DELETE** — DELETE em `/api/DataCenter/{id}`.
   > "E por fim removo o registro."

Crie também um Sensor relacionado a um DataCenter para mostrar o relacionamento entre tabelas.

---

## 4:20 — Evidência de persistência com SELECT no banco (45s)

> "Para comprovar que os dados realmente persistem no banco, conecto diretamente no container do PostgreSQL e rodo um SELECT."

```bash
docker container exec -it db-RM561857 psql -U pmuser -d pmdb -c "SELECT * FROM datacenter;"
docker container exec -it db-RM561857 psql -U pmuser -d pmdb -c "SELECT * FROM sensor;"
docker container exec -it db-RM561857 psql -U pmuser -d pmdb -c "\dt"
```

> "Aqui estão os registros gravados, consultados diretamente no banco. O comando \dt mostra todas as tabelas da solução, com mais de duas tabelas relacionadas entre si."

Esta etapa é obrigatória — sem ela há penalidade de 2 pontos.

---

## 5:05 — Fechamento (20s)

> "Recapitulando: temos dois containers Docker integrados na mesma rede, rodando em nuvem na Azure, com imagem personalizada, usuário não privilegiado, volume nomeado para persistência, variáveis de ambiente, portas expostas e CRUD completo persistindo em múltiplas tabelas. Todo o passo a passo está no README do repositório. Obrigado."

---

## Checklist antes de gravar

- [ ] Estou conectado na Azure VM, não no localhost (mostrar hostname/IP)
- [ ] Portas 8080 e 5432 liberadas no NSG da Azure
- [ ] Trocar `<URL_DO_REPO>` e `<IP_PUBLICO_DA_VM>` pelos valores reais
- [ ] Áudio testado, tela em 720p ou mais
- [ ] Demonstrei: clone, up -d --build, ps, logs, exec (pwd/ls/whoami), CRUD, SELECT
- [ ] whoami no app retorna pmuser (não root)
- [ ] Sem cortes escondendo etapas

## Checklist antes de entregar no portal

- [ ] PDF com página de rosto (este pacote: Entrega-DevOps-GS-2026-1.pdf), com links preenchidos
- [ ] Link do GitHub no PDF e no README
- [ ] Link do vídeo no YouTube no PDF e no README
- [ ] Arquivo .txt com RM, nome e turma de cada integrante dentro do .zip
- [ ] Trocar a senha do banco Oracle FIAP que estava exposta no repositório
