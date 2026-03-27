#include <stdio.h>

// Versão recursiva
int seq_recursiva(int n) {
    if (n <= 10)
        return 10;
    else
        return 2*n - seq_recursiva(n-1) + 4;
}

// Versão desrecursivada (iterativa)
int seq_iterativa(int n) {
    if (n <= 10)
        return 10;

    int resultado = 10; // Seq(10) = 10
    for (int i = 11; i <= n; i++) {
        resultado = 2*i - resultado + 4;
    }
    return resultado;
}

int main() {
    int n;
    printf("Introduza um numero inteiro positivo: ");
    scanf("%d", &n);

    if (n <= 0) {
        printf("Erro: o numero deve ser positivo.\n");
        return 1;
    }

    printf("Seq(%d) [recursiva]  = %d\n", n, seq_recursiva(n));
    printf("Seq(%d) [iterativa]  = %d\n", n, seq_iterativa(n));

    return 0;
}
