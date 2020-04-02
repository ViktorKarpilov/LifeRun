
from abc import ABC, abstractmethod


class IStaticAnalizer(ABC):
    @abstractmethod
    def AnalizeSOLID(self, stream):
        pass

    @abstractmethod
    def AnalizeCodeReadability(self, stream):
        pass

    @abstractmethod
    def AnalizeMomoryCorruption(self, stream):
        pass

    @abstractmethod
    def AnalizeDependency(self, stream):
        return bool

    @abstractmethod
    def construct(cls, *args, **kwargs):
        return cls(*args, **kwargs)
