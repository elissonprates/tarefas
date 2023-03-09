export interface IRetornoApi {
    sucesso?: boolean;
    mensagens?: string[];
}

export class RetornoApi<T> implements IRetornoApi {
    sucesso?: boolean;
    mensagens?: string[];
    dados: T[];
}
