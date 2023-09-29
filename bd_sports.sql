CREATE DATABASE bd_sports
    WITH
    OWNER = postgres
    ENCODING = 'UTF8'
    CONNECTION LIMIT = -1
    IS_TEMPLATE = False;
	
	CREATE SCHEMA sys
    AUTHORIZATION postgres;
	

	create table if not exists sys.usuario_tb
	(
		id_usuario serial not null primary key,
		login varchar not null,
		normalizedLogin varchar,
		senha varchar not null,		
		email varchar(300)		,
		tipo_usuario integer default 0
	);	
	


	create table if not exists sys.usuario_claim_tb(
		id_usuario bigint not null,
		claim varchar not null,
		constraint id_usuario_fk foreign key(id_usuario)
		references sys.usuario_tb(id_usuario)
	);
	
	
	create table if not exists sys.club_tb(
		id_club serial not null primary key,
		id_usuario bigint null,
		nome_fantasia varchar,
		razao_social varchar,
		cpf_cnpj bigint,
		is_pj boolean,
		endereco varchar,
		numero integer,
		cidade varchar,
		cep varchar(20),
		uf varchar(2),
		imagem_base64 varchar,
		is_verificado boolean,
		data_fundacao timestamp null,
		nome_presidente varchar null
	);		
	

	create table if not exists sys.aluno_tb(
		id_aluno serial not null primary key,
		id_usuario bigint,
		id_club bigint null,
		nome varchar not null,
		documento varchar,
		cpf_cnpj bigint,
		foto_base64 varchar,
		data_nascimento timestamp,
		email varchar(300),
		telefone varchar(50),
		celular varchar(50),
		is_pcd BOOLEAN,
		descricao_pcd varchar, 
		posicao_jogador varchar(50),		
		endereco varchar,
		numero integer,
		cidade varchar,
		uf varchar,
		cep varchar,
		complemento varchar(200),
		camiseta_numero integer,
		is_verificado boolean null,
		CONSTRAINT id_usuario_fk foreign key (id_usuario)
		references sys.usuario_tb(id_usuario)	,
		CONSTRAINT id_club_fk foreign key (id_club)
		references sys.club_tb(id_club)	
	);
	

	create table if not exists sys.responsavel_aluno_tb(
		id_responsavel serial not null primary key,
		id_aluno bigint not null,
		nome_responsavel varchar(300),
		documento_responsavel varchar,
		cpf_responsavel bigint,
		grau_parentesco varchar,
		Constraint id_aluno_fk foreign key (id_aluno)
		references sys.aluno_tb(id_aluno)
	);
	
	create table if not exists sys.solicitacao_aluno_club_tb(
		id_solicitacao serial not null primary key,
		id_aluno bigint not null,
		id_club bigint not null,
		data_solicitacao timestamp not null,
		is_aceito boolean null,
		is_visto boolean null,
		constraint id_aluno_fk foreign key(id_aluno)
		references sys.aluno_tb(id_aluno),
		constraint id_club_gk foreign key(id_club)
		references sys.club_tb(id_club)
	);
	
	create table if not exists sys.notificacao_tb(
		id_notificacao serial not null primary key,
		id_aluno bigint  null,
		id_club bigint null,
		data_notificacao timestamp not null,
		is_visto boolean null,
		tipo_usuario integer not null,
		notificacao varchar not null,
		imagem_notificacao varchar null,
		link_redirect varchar null,
		constraint id_aluno_fk foreign key (id_aluno)
		references sys.aluno_tb(id_aluno),
		constraint id_club_fk foreign key (id_club)
		references sys.club_tb(id_club)
	);
	
	

	
	create table if not exists sys.aluno_club_tb(
		id_aluno bigint not null,
		id_club bigint not null,
		data_ingresso timestamp null,
		gols_marcados integer null
	);
	
	
	create table if not exists sys.agenda_evento_tb(
		id_evento serial not null primary key,
		data_hora_evento timestamp not null,
		tipo_evento integer not null,
		id_club1 bigint not null,
		id_club2 bigint null,
		endereco_resumido varchar,
		imagem_base64 varchar,
		observacoes varchar(300),
		constraint id_club1_fk foreign key(id_club1)
		references sys.club_tb(id_club),
		constraint id_club2_fk foreign key(id_club2)
		references sys.club_tb(id_club)
	);
	
	--Tornar usuario administrador
insert into sys.usuario_claim_tb (id_usuario, claim) values (1, 'adm')

--Criar Role usuario Club
insert into sys.usuario_claim_tb(id_usuario, claim) values (1, 'club');
insert into sys.usuario_claim_tb(id_usuario, claim) values (1, 'upd-club');
insert into sys.usuario_claim_tb(id_usuario, claim) values (1, 'del-club');
insert into sys.usuario_claim_tb(id_usuario, claim) values (1, 'read-club');
	
	
	
	
--Querys testes
delete from sys.usuario_tb
delete from sys.usuario_claim_tb
delete from sys.aluno_tb
delete from sys.responsavel_aluno_tb

select * from sys.solicitacao_aluno_club_tb
select * from sys.responsavel_aluno_tb
	select* from sys.aluno_tb where id_aluno = 12
	select  * from sys.usuario_tb where id_usuario = 33 order by id_usuario desc
	
	select id_responsavel as Id, id_aluno as IdAluno, nome_responsavel as Nome, documento_responsavel as Documento, cpf_responsavel as CpfCnpj, grau_parentesco as GrauParentesco 
	from sys.responsavel_aluno_tb where id_aluno = 12
	select * from sys.usuario_claim_tb
	select * from sys.club_tb
	select * from sys.club_tb where id_club = 33
	
	select * from sys.club_tb where id_usuario =33

	select * from sys.solicitacao_aluno_club_tb where id_club = 23 LIMIT 50
select * from sys.solicitacao_aluno_club_tb where id_club = 23 order by data_solicitacao desc LIMIT 50
	drop table sys.usuario_tb
	drop table sys.usuario_claim_tb
	drop table sys.club_tb
	drop table sys.aluno_tb
	drop table sys.responsavel_aluno_tb
	
	
	insert into sys.notificacao_tb (id_aluno, id_club, data_notificacao, is_visto, tipo_usuario, notificacao, imagem_notificacao,
									link_redirect) values (22, 23, now(), false, @tipo_usuario, @notificacao,
														   @imagem_notificacao, @link_redirect) returning id_notificacao

