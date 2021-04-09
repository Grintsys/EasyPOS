import { FuseUtils } from '@fuse/utils';

export class Configuration {
    code: number;
    key: string;
    value: string;

    /**
     * Constructor
     *
     * @param configuration
     */
    constructor(configuration?)
    {
        configuration = configuration || {};
        this.code = configuration.code || FuseUtils.generateGUID();
        this.key = configuration.key || '';
        this.value = configuration.value || '';
    }

    /**
    * Add category
    *
    * @param {MatChipInputEvent} event
    */
    getInitialData(): Configuration[]
    {
        return [
            new Configuration({key: 'Key01', value: 'Value01'}),
            new Configuration({key: 'Key02', value: 'Value02'}),
            new Configuration({key: 'Key03', value: 'Value03'}),
            new Configuration({key: 'Key04', value: 'Value04'}),
            new Configuration({key: 'Key05', value: 'Value05'}),
            new Configuration({key: 'Key06', value: 'Value06'}),
            new Configuration({key: 'Key07', value: 'Value07'}),
            new Configuration({key: 'Key08', value: 'Value08'}),
            new Configuration({key: 'Key09', value: 'Value09'}),
            new Configuration({key: 'Key10', value: 'Value010'})
        ]
    }
}


