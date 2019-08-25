import re
import subprocess
from typing import List

from tools.lib.models.mode import Mode

__all__ = ('VersionManager',)


class VersionManager:
    def __init__(self, mode: Mode):
        self._version_regex: str = r'(\d+)\.(\d+)\.(\d+)(-\w+)?'
        self._package_name: str = mode.package_name
        self._major: bool = mode.major
        self._minor: bool = mode.minor
        self._patch: bool = mode.patch
        self._flag: str = mode.flag

    def _get_package_info(self) -> List[str]:
        """Retrieve package info from nuget"""
        out = subprocess.Popen(['nuget', 'list', 'id:{0}'.format(self._package_name), '-Prerelease'],
                               stdout=subprocess.PIPE,
                               stderr=subprocess.STDOUT)

        stdout, stderr = out.communicate()
        return stdout.decode('utf8').split('\r\n')

    def get_next_version(self) -> str:
        """Get next version number of package"""
        current_version: str = self._get_package_info()[0]
        major, minor, patch, flag = re.findall(self._version_regex, current_version)[0]
        if self._major:
            major = int(major) + 1
        if self._minor:
            minor = int(minor) + 1
        if self._patch:
            patch = int(patch) + 1
        if self._flag is not None:
            flag = self._flag
        return '{0}.{1}.{2}{3}'.format(major, minor, patch, flag)
