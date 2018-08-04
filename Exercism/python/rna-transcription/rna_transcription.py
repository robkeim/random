from string import maketrans

def to_rna(dna_strand):
    translations = maketrans("GCTA", "CGAU")
    return dna_strand.translate(translations)
