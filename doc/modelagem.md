# Modelagem

O que é:
É uma aplicação cuja função é gerenciar os pacientes de um psicólogo.

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
  - Situação (ativado, desativado)
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
