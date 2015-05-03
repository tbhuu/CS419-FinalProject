# CS419 - Introduction to Information Retrieval - Final Project #

----------

## Brief introduction ##
- Information retrieval system on Vietnamese News dataset.
- Queries can be with-accent and non-accent.
- Implicit feedback from user by clicking links.
- N-gram algorithm is applied.

## Installation and Testing ##

Software:

- Visual Studio 2013 (if use VS Express -> Desktop app version).

Setup:

- Put dataset into Resources folder (i.e. Resources/DTR0503301823.txt).
- Run the project using Visual Studio. 
- For first run, click button **Run Indexer** to build inverted index for dataset (latter run can reuse existing index file).
- After the `Indexer` finish running, input query in `TextBox` and click **Search** to search.
- In **Result Dialog**, after clicking on some relevant documents, click **Refresh** to view the revised result using the relevant documents (documents those are clicked).


