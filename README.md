# Axon record linkage

Scope

- matching tracking cookings
- database criminal investigation
- preparing research data from differen sources

How it works

Axon is developed to link datasets that cannot be linked on hard or even soft criteria. Axon links datasets by evaluating and then applying Bayesian relations between fields in both sets.
Axon uses a two step approach.
Step 1	The records are linked on hard criteria like city and month of birth. Each record form dataset 1 has then several candidate records in dataset 2.
Step 2	Axon derives Bayesian relations between the content in other fields like telephone numbers called, disease code in set 1 and care code in set 2 or income in set 1 and type of neighborhood in set 2. Bayesian relations are calculated between all fields in both sets.
Step 3	Axon uses the derived knowledge to match the datasets again. The matching now becomes better. The number of candidate found are now less than in step 1. Then Axon derives new estimations based on the new match and matches again. Now the match is even better and the number of candidates shrinks even more. Axon can proceed with this until there are no gains found. Mostly three to four loops are sufficient.   

Conditions

There has to be a relation between field in set 1 and set 2. If for example set 1 uses a different identity number for shops then in set 2 a relation exists. Or set 1 uses a disease coding system and set 2 a care coding system. The relation is only not yet known.
The system can handle billions of records, at least 256 fields in both sets and 32000 categories in each field. The only limit is the ram memory of your computer/server.
Axon uses a Bayesian or a Chi Square estimation. Therefore the estimation is based on categories, not on ratio’s. A field like income has to be converted to categories.

Prepare data set

Axon expects a dataset with simple headers to point to the nummber of independent, dependent and singular variables
In the example below there are 5 independent, 6 dependent and one singular variables (5/6/1) with their names, starting with the name 'InDep0'. The name should befollowed with only the delimer (asumming the missing value is an empty field) or a catagory (like 'InDep3' and 'InDep4').
The data set is arranged in groups. The first record is always the first record of the independent dataset. The records after that are the candidates (after a raw matching with the independen dataset). Both the independent and the dependent records start with the block number followed by the identifying key of the record. Then the field values follow.
Only the dependent records have singular fields values (after the dependent field values)

Choose the Test tab to build a testset. You can choose the number of field (variables) and the number of records. It is recommended to do is to test wether the dataset you are planning to input can be handled by the RAM of your server or desktop.


example.prt
______________________________________________________
5/6/1

InDep0/
InDep1/
InDep2/
InDep3/miss
InDep4/99

Dep0/
Dep1/
Dep2/
Dep3/
Dep4/
Dep5/

Sing0/

1/-5175954396074827776/C9/C1/C8/C11/C1/
1/-5175954396074827776/C6/C2/C9/C12/C2/C1/C1/
1/-5175954396074827776/C2/C19/C17/C12/C2/C17/C0/
1/-5175954396074827776/C1/C5/C6/C11/C10/C8/C0/

2/3073208371991312384/C3/C11/C0/C3/C13/
2/3073208371991312384/C10/C12/C1/C4/C14/C1/C0/
2/3073208371991312384/C7/C13/C2/C6/C9/C12/C0/
_____________________________________________________

Literature

There is a lot of academic literature on this topic. However the concept record linkage can apply to other linkage methods like linking on Soundex’s and distances.
Next references are about the method used in Axon:
 

