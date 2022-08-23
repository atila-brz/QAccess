## __Padrões de Commit__

Example:

```fix: side menu working```

### Tipo e Descrição

O commit semântico possui os elementos estruturais abaixo (tipos), que informam a intenção do seu commit ao utilizador(a) de seu código.

- `feat`- Commits do tipo feat indicam que seu trecho de código está incluindo um **novo recurso** (se relaciona com o MINOR do versionamento semântico).

- `fix` - Commits do tipo fix indicam que seu trecho de código commitado está **solucionando um problema** (bug fix), (se relaciona com o PATCH do versionamento semântico).

- `docs` - Commits do tipo docs indicam que houveram **mudanças na documentação**, como por exemplo no Readme do seu repositório. (Não inclui alterações em código).

- `test` - Commits do tipo test são utilizados quando são realizadas **alterações em testes**, seja criando, alterando ou excluindo testes unitários. (Não inclui alterações em código)

- `build` - Commits do tipo build são utilizados quando são realizadas modificações em **arquivos de build e dependências**.

- `perf` - Commits do tipo perf servem para identificar quaisquer alterações de código que estejam relacionadas a **performance**.

- `style` - Commits do tipo style indicam que houveram alterações referentes a **formatações de código**, semicolons, trailing spaces, lint... (Não inclui alterações em código).

- `refactor` - Commits do tipo refactor referem-se a mudanças devido a **refatorações que não alterem sua funcionalidade**, como por exemplo, uma alteração no formato como é processada determinada parte da tela, mas que manteve a mesma funcionalidade, ou melhorias de performance devido a um code review.

- `chore` - Commits do tipo chore indicam **atualizações de tarefas** de build, configurações de administrador, pacotes... como por exemplo adicionar um pacote no gitignore. (Não inclui alterações em código)

- `ci` - Commits do tipo ci indicam mudanças relacionadas a **integração contínua** (*continuous integration*).

#

## Nomeclaturas para Branchs:

Example:

```feature/list-matches```

### Tipo e Descrição

Uso prefixos pré-definidos para criar uma branch. Segue abaixo a lista:

- ```__bugfix/__``` - Como o próprio nome já diz, é um BUG e precisa ser corrigido de forma imediata, o quanto antes. Num outro artigo eu explico melhor a utilização desse cara e branches principais.

- ```__feature/__``` - O nome já diz também o que é, uma nova feature que será adicionada ao projeto, componente e afins.

- ```__hotfix/__``` - Às vezes esse termo pode ser usado de outras formas, até mesmo para usar no lugar do bugfix. Porém, eu prefiro separar, deixar com semânticas diferentes. Ele é bem similar ao bugfix/, porém, ele não é um BUG, mas sim uma correção, seja ela de cor, textos, alterações não tão urgentes, que não signifiquem BUG's.

- ```__improvement/__``` - O nome já mostra para o que serve. Em si é uma melhoria para um fodasse já existente, seja de performance, de escrita, de layout, etc.