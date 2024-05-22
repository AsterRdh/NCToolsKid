package nc.vo.ceri.enums.itf;

import nc.md.model.impl.MDEnum;

public interface IBaseEnum <T extends MDEnum,E> {

    public T getMDEnum();

    public E getNcValue() ;

    public String getDisplayName();


}
