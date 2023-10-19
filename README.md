# Modelagem e Projeto

O que é:
É uma aplicação cuja função é gerenciar os pacientes de um profissional.

## Designer pattern do projeto

Buscou-se neste projeto usar de alguns conceitos do clean arquitecture e alguns dos conceitos do DDD, como Entidades, agregados, objetos de valor e o dominio rico.
De forma que, no módulo de domain estão as regras de negócio, o equivalente a camada entities do clean architecture.
Na camada de aplicação, estão os use cases, que representam as intensões do "usuário", e as interfaces dos gateways/serviços/repositórios cujas implementações estão na camada de infrastructure.

## domain

- Estão as regras de negócio, as entidades, possíveis eventos de domínio, entidades de negócio, objetos de valor.
- Entidade é tudo que na visão de negócio há identificação.
- É comum que as entidades possuem objetos de valor, que são propriedades imutáveis das entidades, por exemplo o Voucher.
- É importante dizer que estas entidades não são as entidade do banco de dados, são entidades do negócio.

## application

- Estão os casos de uso, representação da intensão de usuário, entendendo como usuário tudo que for usar este sistema.
- Estão as interfaces dos gateways/serviços/repositórios, contratos claros do que o sistema precisa para orquestrar as regras da aplicação e executar a intensão do usuário.

## infraestructure

- É nesta e somente nesta camada que as decisões de ferramentas, tecnologias, framework são tomadas, tal como, qual banco, qual ORM, qual framwork, versões, serviços externos e etc.
- São feitas as implementações necessárias para executar os usecases.
- Nesta camada há por exemplo os Controllers da api rest.
- Podem ser implementado nesta camada presenters para diferentes tipos resposta de api.
- Aqui define/implementa-se as formas de servir gRPC, REST, SOAP, CLI.

O fluxo de dados basicamente é:

```cliente -> infrastructure -> application -> domain```

## Linguagem ubíqua - glossário

trata-se da linguagem universal do negócio, são os "nomes dos bois".
quando o negócio já existe é a linguagem que os departamentos usam pra se comunicar, neste caso como ainda não existe são os nomes das "coisas".

- Profissional
  - Uma profissional de saúde Psicólogo, Psiquiatra, Psicanalista.
- Paciente
  - Pessoa que é assistida e gerenciada pelo Profissional.
- Ficha do paciente
  - Informações pessoais do paciente, endereço, nome, número dos pais e etc.
- Sessão
  - Encontro marcado entre profissional e paciente(s) com horário e tempo definido.
- Anotações
  - Notas da sessão.

## Agregados, entidade, objetos de valor

- Profissional.
  - Profissional (entidade root)
  - Paciente (entidade)
    - Ficha do paciente (objeto de valor)
- Sessão.
  - Sessão (entidade)
  - Anotações (objeto de valor)

## Agregado Profissional

- Profissional
  - Id
  - Registro Profissional
  - Nome
  - E-mail
  - Documento
  - Paciente[] (ids)

- Paciente
  - Id
  - Nome
  - E-mail
  - Documento
  - Ativado
  - Ficha do paciente

## Agregado de Sessão

- Sessão
  - Paciente[] (ids)
  - Profissional (id)
  - Data e Hora marcada
  - Situação (marcada, realizada, cancelada)
  - Duração prevista
  - Histórico
  - Anotações
    - Conteúdo

## Intensões e regras

### Profissional

- Editar seus dados
  - O documento não pode ser editado.
- Incluir paciente
  - Não pode existir dois pacientes com mesmo documento ou e-mail
  - O Paciente inicia ativado
  - Todos os campos do paciente são obrigatórios e válidos. Menos as informações da ficha, inicia em branco.
- Desativar paciente
- Ativar paciente
- Preencher Ficha do paciente
- Cadastrar sessão
  - Paciente, Data e hora marcada, duração prevista são obrigatórios.
  - Caso Data for no passado a situação é:  realizada.
  - Caso Data for no futuro a situação é: marcada;
- Incluir anotação na sessão
  - Conteúdo não pode ser vazio.
- Cancelar Sessão
- Adiar Sessão
  - Edita a data marcada, incluir no histórico "adiada da data tal para data tal"
    - Caso Data for no passado a situação é:  realizada.
    - Caso Data for no futuro a situação é: marcada;

## Executar localmente

- na pastar infrastructure há uma pasta docker, nesta há um docker compose, para subir o banco, basta ir para a pasta e executar o container.

  ```bash
  cd infrastructure/docker
  docker compose up -d
  ```

  ```bash
  docker ps
  ```

  ```bash
  cd ../..
  dotnet run --project infrastructure/api/api.csproj
  ```
