# MyGL

![](/docs/GL-FinancialYearReportExpanded.jpg)

**MyGL** is a personal General Ledger .Net webapp that categorises and provides pivot reports on your bank account transactions.

## Features
  - Categorises transactions
  - Customisable categories and search string rules
  - General Ledger pivot tables per financial or calendar year
  - Customisable pivot tables
  - Import multiple .csv files at once
  - Re-import data without duplication

## Setup
The simplest way to setup MyGL is using **docker-compose**.

Create a separate directory for MyGL and place a copy of ```docker-compose.yml``` from this repo.

Next, copy ```.mygl.env.example``` (again from this repo) to ```.mygl.env``` and adjust settings as required. Be sure to at least change ```SA_PASSWD``` to a secure password of your choosing.

Run:
  
```docker-compose up```  
  
To access, go to http://localhost:9999 (or to the port you set in ```.mygl.env```)