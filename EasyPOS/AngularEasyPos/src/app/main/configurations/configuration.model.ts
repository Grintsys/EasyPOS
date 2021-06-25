import { FuseUtils } from '@fuse/utils';

export class ConfigurationDto {
    id: string;
    key: string;
    value: string;

    constructor(configuration?)
    {
        configuration = configuration || {};
        this.id = configuration.id || '';
        this.key = configuration.key || '';
        this.value = configuration.value || '';
    }
}


