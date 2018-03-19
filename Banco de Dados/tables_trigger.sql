CREATE TABLE ApiProfessor (
	idProfessor uniqueidentifier default newid() PRIMARY KEY,
	nome		nvarchar(50) NOT NULL,
	email		nvarchar(50) NOT NULL 
)

CREATE TABLE ApiProjeto (
	idProjeto   uniqueidentifier default newid() PRIMARY KEY,
	nome	    nvarchar(100)	 NOT NULL,
	descricao   ntext			 NOT NULL,
	ano		    int				 NOT NULL,
	idProfessor uniqueidentifier NOT NULL
)

CREATE TABLE ApiAluno (
	ra		  char(5)		   PRIMARY KEY,
	nome	  nvarchar(50)	   NOT NULL,
	email     nvarchar(50)	   NOT NULL,
	idProjeto uniqueidentifier NOT NULL
)

ALTER TABLE ApiAluno ALTER COLUMN idProjeto uniqueidentifier NULL

ALTER TRIGGER deletaProjeto_tg
	ON ApiProjeto
	INSTEAD OF DELETE
AS
	UPDATE ApiAluno
		SET idProjeto = NULL
		WHERE idProjeto IN (SELECT deleted.idProjeto FROM deleted)
	DELETE FROM ApiProjeto WHERE idProjeto IN (SELECT deleted.idProjeto FROM deleted)
GO

insert into ApiProjeto(nome, descricao, ano, idProfessor) values ('nome', 'descricao',2018, (select idProfessor from ApiProfessor where nome like '%Simone%'))
select * from ApiProjeto
update ApiAluno set idProjeto = (select idProjeto from ApiProjeto where nome = 'nome') where nome = 'Felipe'
select * from ApiAluno
delete from ApiProjeto