from dataclasses import dataclass
from typing import List

from tools.lib.consts.argv_names import ArgvNames
from tools.lib.models.argv import Argv

__all__ = ('Mode', 'ModeParser', 'ModeValidator')


@dataclass
class Mode:
    package_name: str = None
    major: bool = False
    minor: bool = False
    patch: bool = True
    flag: str = None


class ModeValidator:
    @staticmethod
    def validate(mode: Mode):
        assert mode.package_name is not None, 'Invalid package name. Value cannot be None'
        assert len(mode.package_name) > 0, 'Invalid package name. Length shouldn`t be equal 0'
        assert mode.major or mode.minor or mode.patch, 'You have to specify version update mode'


class ModeParser:
    def __init__(self, argv: Argv):
        self.argv: Argv = argv

    def parse(self) -> Mode:
        result: Mode = Mode()
        result.package_name = self._parse_package_name()
        result.major = self._parse_flag(ArgvNames.MAJOR)
        result.minor = self._parse_flag(ArgvNames.MINOR)
        result.patch = self._parse_flag(ArgvNames.PATCH)
        result.flag = self._parse_kwargs(ArgvNames.FLAG)

        if result.flag is not None and result.flag[0] != '-':
            result.flag = f'-{result.flag}'

        ModeValidator.validate(result)

        return result

    def _parse_package_name(self):
        for argv_name in ArgvNames.PACKAGE_NAME:
            package_name = self.argv.kwargs.get(argv_name, None)
            if package_name is not None:
                return package_name

        if len(self.argv.positional) > 0:
            return self.argv.positional[0]

        return None

    def _parse_flag(self, argv_names: List[str], default_value: bool = False):
        for argv_name in argv_names:
            flag_value = self.argv.flags.get(argv_name, None)
            if flag_value is not None:
                return flag_value

        return default_value

    def _parse_kwargs(self, argv_names: List[str]):
        for argv_name in argv_names:
            kwarg_value = self.argv.kwargs.get(argv_name, None)
            if kwarg_value is not None:
                return kwarg_value

        return None
