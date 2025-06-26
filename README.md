# MeuLivroDeReceitas

Aplicativo de Receitas com IA (C# & DDD) — Em Desenvolvimento

Este projeto é uma API RESTful para um aplicativo de receitas, que permite operações CRUD (criar, ler, atualizar e deletar receitas) e também a geração de novas receitas utilizando Inteligência Artificial (GPT). Além da gestão de receitas, a aplicação conta com um sistema completo de cadastro de usuários, autenticação, alteração e recuperação de senha, garantindo segurança e praticidade para os usuários.

O objetivo é facilitar o gerenciamento e a descoberta de receitas de maneira moderna e inteligente, utilizando boas práticas de arquitetura e metodologias ágeis.

## Principais Tecnologias

- **C#**
- **ASP.NET Core**
- **Domain-Driven Design (DDD)**
- **GPT (OpenAI) para geração de receitas**
- **Azure DevOps** para integração contínua e gestão ágil (Scrum)
- **xUnit** e **FluentAssertions** para testes de unidade e integração

## Funcionalidades

- Cadastro, edição, exclusão e listagem de receitas
- Geração automática de receitas com IA
- CRUD completo de usuários
- Autenticação e autorização de usuários
- Recuperação e alteração de senha
- Estrutura baseada em boas práticas de DDD para facilitar manutenção e evolução do sistema

## Testes

O projeto possui testes de unidade e integração implementados utilizando xUnit e FluentAssertions, garantindo a confiabilidade e robustez do sistema.

Para rodar os testes:
```bash
dotnet test
```

## Como executar

> ⚠️ Projeto em desenvolvimento: instruções sujeitas a alterações.

1. Clone o repositório:
    ```bash
    git clone https://github.com/JoaoPauloPinheiroM/MeuLivroDeReceitas.git
    ```
2. Abra a solução no Visual Studio ou VS Code
3. Restaure as dependências:
    ```bash
    dotnet restore
    ```
4. Execute o projeto:
    ```bash
    dotnet run --project src/MeuLivroDeReceitas.API
    ```

## Contribuição

Contribuições são bem-vindas! Sinta-se à vontade para abrir issues ou pull requests.

## Gestão do Projeto

A gestão do projeto é realizada via Azure DevOps, utilizando a metodologia Scrum para garantir entregas ágeis e iterativas.

---

Desenvolvido por [João Paulo Pinheiro](https://github.com/JoaoPauloPinheiroM)
