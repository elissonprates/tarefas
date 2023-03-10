import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
    name: 'zeroEsquerda'
})
export class ZeroAEsquerdaPipe implements PipeTransform {

    transform(value: number, args?: any): any {

        var valueString = value.toString();
        while (valueString.length < args) valueString = "0" + valueString;
        return valueString;
    }

}
