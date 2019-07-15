from textwrap import wrap

def proteins(strand):
    codons = wrap(strand, 3)

    result = []

    for codon in codons:
        if codon == "AUG":
            result.append("Methionine")
        elif codon == "UUU" or codon == "UUC":
            result.append("Phenylalanine")
        elif codon == "UUA" or codon == "UUG":
            result.append("Leucine")
        elif codon == "UCU" or codon == "UCC" or codon == "UCA" or codon == "UCG":
            result.append("Serine")
        elif codon == "UAU" or codon == "UAC":
            result.append("Tyrosine")
        elif codon == "UGU" or codon == "UGC":
            result.append("Cysteine")
        elif codon == "UGG":
            result.append("Tryptophan")
        elif codon == "UAA" or codon == "UAG" or codon == "UGA":
            break
        else:
            raise Exception("Invalid codon value: " + codon)

    return result
