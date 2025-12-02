-- init-db.sql
-- Cria schema e tabela para armazenar perguntas como JSONB em dbo.questions

CREATE SCHEMA IF NOT EXISTS dbo;

CREATE TABLE IF NOT EXISTS dbo.questions (
    id BIGSERIAL PRIMARY KEY,
    data JSONB NOT NULL,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP
);

-- Índice GIN para acelerar consultas JSONB
CREATE INDEX IF NOT EXISTS idx_questions_data_gin ON dbo.questions USING GIN (data);

-- Índice para procurar pelo campo interno 'id' no JSON (se usado)
CREATE INDEX IF NOT EXISTS idx_questions_data_id ON dbo.questions (((data->>'Id')));

-- Exemplo: inserir uma linha de teste
-- INSERT INTO dbo.questions (data) VALUES ('{"Id":1, "Description":"Exemplo","Competencies":[1,2], "Options":[], "RightAwnser":1}');
