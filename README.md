# Teste MSA

A demanda foi executada com as seguintes premissas:
- Backend - .Net Core 5
- Database  - MsSQL 2019
- ORM - NHibernate 
- Frontend - Angular 13 / Bootstrap 4

Foi utilizada a biblioteca `br.com.ussolucoes.persistence` para uso e gerencimento do Nhibernate, ela contém o controle de sessão, classe base como os comando de crud e consulta, classe helper para criação e atualização de banco de dados.

O banco de dados foi gerado com uso do NHibernate.Tool.hbm2ddl, e teve o seguinte output de execução:
```
if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_1EA6B907]') and parent_object_id = OBJECT_ID(N'tbNh_telefone'))
alter table tbNh_telefone  drop constraint FK_1EA6B907

if exists (select * from dbo.sysobjects where id = object_id(N'tbNh_cliente') and OBJECTPROPERTY(id, N'IsUserTable') = 1) 
  drop table tbNh_cliente
if exists (select * from dbo.sysobjects where id = object_id(N'tbNh_telefone') and OBJECTPROPERTY(id, N'IsUserTable') = 1) 
  drop table tbNh_telefone
  
create table tbNh_cliente (Id UNIQUEIDENTIFIER not null, cli_nome NVARCHAR(100) null, cli_email NVARCHAR(255) null, cli_dataNascimento DATETIME2 null, primary key (Id))

create table tbNh_telefone (Id UNIQUEIDENTIFIER not null, tel_ddd NVARCHAR(3) null, tel_numero NVARCHAR(10) null, tel_tipo INT null, cli_id UNIQUEIDENTIFIER null, primary key (Id))

alter table tbNh_telefone add constraint FK_1EA6B907 foreign key (cli_id) references tbNh_cliente

```

### br.com.ussolucoes.persistence
Biblioteca própria que faz a parte de gestão de sessão e padronização do CRUD e consultas usando NHibernate.
